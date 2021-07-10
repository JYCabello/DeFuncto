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


Most of what this library is made by biased discriminated unions. An [Option](option.md) is just