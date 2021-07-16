# Option
Data structure representing a value that might or might not be there.
## The problem, by example
You have a rent-a-car agency and you want to get the full name of the last renter of a car identified by a plate number. In order to achieve that, you need to fetch:
1. The car record, which can or cannot be there.
1. Its last rental, which again, could not be there.
1. The user record, which we can guarantee it would be there, due to our database constraints.
1. The user name, which, due to GDPR regulations, could not be there.

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
### LINQ To the rescue
> Hey, that chaining thing looks cool, but what about those cases where I need to use a value from above later in the calls?

Glad you asked, let's say that
## Hey, where is my value?
## Matching it out
## Iterating
### Bonus track: Choose
