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
You might notice that while the final match looks a bit noisier than our final version, the definition of the union is precise and succint. Ignoring that we can pass any email as a string (which we would solve with a Validation or a Result, but it's out of scope here), we have a structure telling us that you need to pass a number that represents a user ID or an Email.

To achieve the same in C#, we have to add a bit of noise:
```cs
public record UserEmail(string Value);
public record UserID(int Value);
public User Get(UserEmail email) => users.Single(u => u.Email == email.Value);
public User Get(UserID id) => users.Single(u => u.ID == id.Value);
public user Get(Du<UserEmail, UserID> identifier) => identifier.Match(Get, Get);
```

Most of what this library is made by biased discriminated unions. An [Option](option.md) is just