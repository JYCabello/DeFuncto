# Discriminated unions
Data structure representing one of many values which can be folded over a final one.

Let's say that you have these two methods:
```cs
public User GetByEmail(string email) => users.Single(u => u.Email == email);
public User GetByID(int id) => users.Single(u => u.ID == id);
```
Fair and simple: You call one of them if you get one of the values, or the other if you get the other. A method orchestrating this, might look something in this line:
```cs
public User Get(string? email, int? id) => email is null ? GetByID(id.Value) : GetByEmail(email!);
```
If you are not getting chills just by looking at this signature, you might not have thought on this method being called like this:
```cs
var userThatIsDefinitelyThere = Get(null, null);
```
Should you have this picture already in mind and still not getting anxious, you have overcome stress, complexity and human mistake, functional programming is not for you. For the rest of mortal beings, we would like a safer alternative.
```cs
public user Get(Du<string, int> emailOrId) => emailOrId.Match(email => GetByEmail(email), id => GetByID(id));
// Which can be simplified as:
public user Get(Du<string, int> emailOrId) => emailOrId.Match(GetByEmail, GetByID);
```
## The caveat
If you were to do this in F#, it would look something like this.
```fs
type Identifier =
  | Email of string
  | Id of int

let getByEmail email = users |> List.find (fun u -> u.Email = email)
let getById id = users |> List.find (fun u -> u.ID = id)

let get term =
  match term with
    | Email email -> getByEmail email
    | Id id -> getById id
// Which can be simplified as:
let get =
  function
    | Email email -> getByEmail email
    | Id id -> getById id
```
You might notice that while the final match looks a bit more verbose than our C# version, the definition of the union is precise and succint. Ignoring that we can pass any email as a string or a negative number as an ID (which we would solve with a Validation or a Result, but it's out of scope here), we have a structure telling us that you need to pass a number that represents a user ID or an Email.
### The workaround
To achieve the same in C#, we have to add a bit of noise:
```cs
public record UserEmail(string Value);
public record UserID(int Value);
public User Get(UserEmail email) => users.Single(u => u.Email == email.Value);
public User Get(UserID id) => users.Single(u => u.ID == id.Value);
public user Get(Du<UserEmail, UserID> identifier) => identifier.Match(Get, Get);
```
## Exhaustive matching, the core issue
If you are in a version of C# that does not have records, you're bound to use classes for that, which would make for a few extra lines, and there's also that if you are in C# 9, you might be tempted to use a nested record and a switch expression.
```cs
public record UserIdentifier
{
    public record UserEmail(string Value) : UserIdentifier;
    public record UserID(int Value) : UserIdentifier;
}
public User Get(UserIdentifier.UserEmail email) => users.Single(u => u.Email == email.Value);
public User Get(UserIdentifier.UserID id) => users.Single(u => u.ID == id.Value);
public User Get(UserIdentifier identifier) =>
    identifier switch
    {
        UserIdentifier.UserEmail email => Get(email),
        UserIdentifier.UserID id => Get(id),
        _ => throw new ArgumentException(nameof(identifier))
    }
```
Which has the drawback of not being exhaustive, and this is the big win with discriminated unions, you cannot forget to map one of the possible types. If I decide to make phone numbers unique in either F# or C#:
```cs
public record UserEmail(string Value);
public record UserID(int Value);
public record PhoneNumber(string Value);
public User Get(UserEmail email) => users.Single(u => u.Email == email.Value);
public User Get(UserID id) => users.Single(u => u.ID == id.Value);
// Does not compile!
public user Get(Du3<UserEmail, UserID, PhoneNumber> identifier) => identifier.Match(Get, Get);
```
```fs
type Identifier =
  | Email of string
  | Id of int
  | PhoneNumber of string

let getByEmail email = users |> List.find (fun u -> u.Email = email)
let getById id = users |> List.find (fun u -> u.ID = id)

// Does not compile!
let get =
  function
    | Email email -> getByEmail email
    | Id id -> getById id
```
None of these options is valid code anymore, while this one is:
```cs
public record UserIdentifier
{
    public record UserEmail(string Value) : UserIdentifier;
    public record UserID(int Value) : UserIdentifier;
    public record PhoneNumber(string Value) : UserIdentifier;
}
public User Get(UserIdentifier.UserEmail email) => users.Single(u => u.Email == email.Value);
public User Get(UserIdentifier.UserID id) => users.Single(u => u.ID == id.Value);
// DOES compile, no bueno.
public User Get(UserIdentifier identifier) =>
    identifier switch
    {
        UserIdentifier.UserEmail email => Get(email),
        UserIdentifier.UserID id => Get(id),
        _ => throw new ArgumentException(nameof(identifier))
    }
```
Which means that we have to carefully investigate where our class is being used and make sure that every switch expression consuming our type is handling the new case, which can be trivial in small simple applications, which are prone not to exist, but it's from daunting in a large codebase to plain impossible if our type is exposed in a library.

Again, it's all about having the compiler hold your hand: If you change the signature of a method to return a `Discriminated Union` that has different type parameters, the compiler will kick and scream until you have solved every inconsistency instead of having your production code crash when your ~~money bags~~ customers are using the product.

## TL; DR
A discriminated union is a type that can be one of many other types, until you call `Match` on it, forcing you to handle every possible type, is a Schrödinger's variable, representing the uncertainty of which is it, allowing you to leave the decision of what to do to the latest stage of the flow you're working on.

Most of what this library has to offer is made by biased discriminated unions. An [Option](option.md) is just a discriminated union that considers one side "Something" (`Some`) and the other side "Nothing" (`None`). Likewise, a [Result](result.md) considers one of the values the desired result (hence, the name) of an operation (`Ok`), and the other one an error (`Error`).