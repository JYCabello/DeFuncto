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
WIP

## How is it solved

### Mapping
### Binding
### LINQ To the rescue
## Hey, where is my value?
## Matching it out
## Iterating
### Bonus track: Choose
