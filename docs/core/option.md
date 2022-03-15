# Option
Data structure representing a value that might or might not be there.
## The problem, by example
You have a rent-a-car agency and you want to get the full name of the last renter of a car identified by a plate number. In order to achieve that, you need to fetch:
1. The car record, which can or cannot be there.
1. Its last rental, which again, could not be there.
1. The user record, which we can guarantee it would be there, due to our database constraints.
1. The user name, which has to be there.

Should we solve it in a classical, imperative way:
```cs
public string? GetLastRenterName(string plateNumber) {
    var car = FindCar(plateNumber);
    if (car is null)
        return null;
    var lastRental = FindLastRentalForCar(car.ID);
    if (lastRental is null)
        return null;
    var user = GetUser(lastRental.UserID);
    return user.FullName;
}
// Now we have to consume it in a defensive way:
var name = GetLastRenterName("1234AB");
Console.WriteLine(name is null ? "Name not found" : name);
```
## How is it solved
Since the introduction of nullable reference types, we can make that defensiveness something hinted or even required by the type system (by setting the return type of `GetLastRenterName` as `string?`), life is much safer, but the cognitive load remains.
### Mapping
In the case of getting the user from the rental, since we can guarantee that the user is there, we could simplify the last step of the function like this:
```cs
return lastRental is null ? null : GetUser(lastRental.UserID).FullName;
```
While, if we were to use the `Option` datatype:
```cs
using static DeFuncto.Prelude;
//...

public Option<Rental> TryFindLastRentalForCar(int carID) { /* ... */ }
public User GetUser(int ID) { /* ... */ }

public Option<string> GetLastRenterName(string plateNumber) {
    var car = FindCar(plateNumber);
    if (car is null)
        return None;
    return TryFindLastRentalForCar(car.ID)
        .Map(r => GetUser(r.UserID).FullName);
}
// Now we consume it in an exhaustive, safe way:
GetLastRenterName("1234AB")
    .Iter(
        name => Console.WriteLine(name),
        () => Console.WriteLine("Name not found")
    );
```
This returns a data structure that works like the Schrödinger cat's experiment box. The name is there and not there at the same time, and instead of asking for the name, the box asks us what to do in either case, because, without opening it, box states exist within the box.
#### How does it work
There you have a decision tree for `Map`:
- `TryFindLastRentalForCar` returns an `Option<Rental>` in the state:
    1. `Some`: Means that the rental was found. Get the `UserID` to search for the user and get its name, returning an `Option<string>` in the state `Some` (there's no branching here, as the user is always there, so is the name).
    1. `None`: Means that there was no rental to be found. In this case, the lambda qwe passed to `Map` will not be run and the final reuslt would be an `Option<string>` in the state `Some`.

As for `Iter`, think about it as a `ForEach` for a list. You don't run the lambda you pass if the list if empty and you run it once per element if it's not. When running `Iter`, you will only run the `None` lambda (the one not taking a parameter) if the `Option` is in the state `None` and the one taking a parameter (the `Some` lambda) if the option is in the `Some` state.
```cs
Option<string>.Some("There was something")
    .Iter(
        value => Console.WriteLine(value), // This one gets run, printing "There was something".
        () => Console.WriteLine("There was nothing") // Execution does not reach this line.
    );

Option<string>.None
    .Iter(
        value => Console.WriteLine(value), // Execution does not reach this line.
        () => Console.WriteLine("There was nothing") // This one gets run.
    );
```
### Binding
> Wait a second, you quantum charlatan, that only solves part of the problem, I still need to do the null check on the car. This "mapping" thing only works if the next step is guaranteed to be there!

Indeed, that requires a slightly more advanced technique called binding. Where we take one option and we give it logic to transform the content into another option if it is in the `Some` state, returning a new `Option` that depends on the internal logic. If the original `Option` was in the `None` state, you just get a new `Option` that happens to be `None`.

Getting dizzy? Bear with me.
```cs
using static DeFuncto.Prelude;
//...

public Option<Car> TryFindCar(string plateNumber) { /* ... */ }
public Option<Rental> TryFindLastRentalForCar(int carID) { /* ... */ }
public User GetUser(int ID) { /* ... */}

public Option<string> GetLastRenterName(string plateNumber) =>
    TryFindCar(plateNumber)
        .Bind(car => TryFindLastRentalForCar(car.ID))
        .Map(r => GetUser(r.UserID).FullName);
```
> What? What if the car is null? Won't car.ID throw a `NullReferenceException`?

That is handled inside the `TryFindCar` just like it is handled inside `TryFindLastRentalForCar` for the rental. That lambda (`car => TryFindLastRentalForCar(car.ID)`) will only be called when the `Option` returned from `TryFindCar` is in the `Some` state. So you only care about it not being there, inside the place where you produce de `Option`, outside, it's no longer a car that can be there or not, but the possibility of a car abstracted away.
#### Decision tree on it
1. We call `TryFindCar` which returns an `Option<Car>`, which can be:
    1. `Some`, which will call `TryFindLastRentalForCar` inside the `Bind` returning an `Option<Rental>`:
        1. `Some`, resulting on an `Option<Rental>` in the `Some` state.
        1. `None`, resulting on an `Option<Rental>` in the `None` state.
    1. `None` resulting on an `Option<Rental>` in the `None` state.
1. Having an `Option<Rental>`, we call `GetUser(r.UserID).FullName` inside a map, the `Option<Rental>` can be:
    1. `Some`, resulting on getting the user, its name and returing an `Option<string>` in the `Some` state.
    1. `None`, closing on in an `Option<string>` in the `None` state.
#### Code that is easy to change.
Let's say that GDPR comes along and the `User` entity can be anonymized now, meaning that `FullName` can now be `null` or even an empty string. With `Option`, a composable data structure, we just need to add another piece to the puzzle:
```cs
using static DeFuncto.Prelude;
//...
private Option<string> NotEmpty(string value) =>
    string.IsNullOrWhitespace(value) ? None : Some(value);

public Option<string> GetLastRenterName(string plateNumber) =>
    TryFindCar(plateNumber)
        .Bind(car => TryFindLastRentalForCar(car.ID))
        .Bind(r => NotEmpty(GetUser(r.UserID).FullName));
```
### LINQ To the rescue
> Hey, that chaining thing looks cool, but what about those cases where I need to use a value from above later in the calls?

Glad you asked, let's say that you have to perform the following operations (try get means that it could be not there):
- Try get a `User` based on its email.
- Try get a `DeliveryAddress` based on the user ID.
- Try get a `PendingOrder` based on an order ID and a user ID.
- Process the order.

Classic, imperative way:
```cs
public User FindUser(string email) { /* ... */ }
public DeliveryAddress FindDeliveryAddress(int userID) { /* ... */ }
public PendingOrder FindPendingOrder(int userID, int orderID) { /* ... */ }
public void Process(PendingOrder order, DeliveryAddress address) { /* ... */ }

public void ProcessOrderFor(string email, int orderID)
{
    var user = FindUser(email);
    if (user == null)
        return;
    var address = FindDeliveryAddress(user.ID);
    if (address == null)
        return;
    var order = FindPendingOrder(user.ID, orderID);
    if (order == null)
        return;
    Process(order, address);
}
```
With option, one little step at a time:
```cs
public Option<User> TryFindUser(Email email) { /* ... */ }
public Option<DeliveryAddress> TryFindDeliveryAddress(UserID userID) { /* ... */ }
public Option<PendingOrder> TryFindPendingOrder(UserID userID, OrderID orderID) { /* ... */ }
public void Process(PendingOrder order, DeliveryAddress address) { /* ... */ }

public void ProcessOrderFor(Email email, OrderID orderID)
{
    var user = TryFindUser(email);
    var address = user.Bind(u => TryFindDeliveryAddress(u.ID));
    var order = user.Bind(u => TryFindPendingOrder(u.ID, orderID));
    // Feels simpler up to this point, then things go a bit more complicated.
    order
        .Bind(o => address.Map(a => (o, a)))
        .Iter(tuple =>
        {
            var (ord, add) = tuple;
            Process(ord, add);
        });
}
```
Since we need both the order and the address to process it, we see ourselves forced to use a tuple to "transfer" both values when they are on the `Some` state, that is OK, but the way to do it can be a bit distracting. Also, we end binding twice over the user, which is not terrible, but doesn't add up to improve the experience.

There's a last trick on our sleeves, though. Since an `Option` is pretty much a list that can have one or no elements, we can treat it as a collection we can query over with the embedded LINQ syntax:
```cs
public Option<User> TryFindUser(Email email) { /* ... */ }
public Option<DeliveryAddress> TryFindDeliveryAddress(UserID userID) { /* ... */ }
public Option<PendingOrder> TryFindPendingOrder(UserID userID, OrderID orderID) { /* ... */ }
public void Process(PendingOrder order, DeliveryAddress address) { /* ... */ }

public void ProcessOrderFor(Email email, OrderID orderID) =>
(
    from user in TryFindUser(email)
    from address in TryFindDeliveryAddress(user.ID)
    from order in TryFindPendingOrder(user.ID, orderID)
    select (address, order)
).Iter(tuple => Process(tuple.order, tuple.address));
```

## Hey, where is my value?
> What do I do if I want to "recover" the value from inside an `Option`?
You think again, carefully. The whole point of the option is that you don't get to look inside, because if you do, you have to care about the value being there or not. If you have an `Option` you are receiving instructions to handle the possibility of it not being there.

If you are at the end of the road, at the point where you are supposed to consume "the value", and all you have is an `Option`, then you have to rethink, and see how to consume the `Option` itself.

Your options, in general, are:
- Matching (or defaulting): Transforming each possibility into something of a shared type for both states and consume that instead.
- Iterating: Performing an action on one or both possible states.
### Matching it out
#### Match
Let's say that you have an ASP.NET Core API and you want to have a standard `Get` endpoint:
```cs
// In the service.
public Car Find(int id) => /* ... */

// In the controller.
[HttpGet]
public ActionResult Get(int id)
{
    var car = service.Find(id);
    return car == null ? NotFound() : Ok(car);
}
```
Simple enough, that's the ASP.NET Core we know and love, still, you have to remember to do that check everywhere. With an `Option` we can add this simple helper to our base controller and use it in **all** the endpoints that could not find the entity:
```cs
// In the service.
public Option<Car> TryFind(int id) => /* ... */

// In the base controller.
protected ActionResult OptionResult<T>(Option<T> option) =>
    option.Match(Ok, NotFound);

// In the controller.
[HttpGet]
public ActionResult Get(int id) =>
    OptionResult(service.TryFind(id));
```
#### Default
Another possibility is to simply have a default value in case the desired one was not found, which is fairly straightforward for a situation like getting a user's name:
```cs
public Option<User> TryFind(int id) => /* ... */

Console.WriteLine(
    TryFind(42)
        .Map(u => $"Found user with name: {u.Name}")
        .DefaultValue("User not found")
); // Will print "Found user with name: The Answer".
```
### Iterating
Sometimes we want to perform an action only if the value is there, only if it's absent or one action for each case. As explained before, this works exactly like `List<T>.ForEach`, in an empty list, no action is performed while for one that has elements, the code is executed once per element:
```cs
public Option<Order> TryFind(int id) => /* ... */
public void Process(Order order) { /* ... */ }

// Acting on not found:
TryFind(1).Iter(() => logger.Log($"Could not find order number {id}"));
// On found:
TryFind(2).Iter(Process);
// On either:
TryFind(3).Iter(
    Process,
    () => logger.Log($"Could not find order number {id}")
);
```
## Bonus track: Choose
Part or the [collection extenstions](./collection_extensions.md).