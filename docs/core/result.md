# Result
Data structure representing one of two possible values, one for success and another one for failure. Essentially, it's a discriminated union with a bias towards the success value.

## The problem, by example
We got an API endpoint protected by an API Key in the headers. That endpoint requires:
1. The key to be in the headers.
1. To be attached to an active user.
1. For the user to have the appropriate role.
1. For the data to be there

We want to return a different result depending on any "exit condition", be it any of the requirements not being met all of them being there.

You already know how to solve this with conditionals and early returns, don't you? (I'll be using ASP.NET Core for the examples).

I know that ASP.NET has built-in security, let's pretend it doesn't.

```cs
[HttpGet(Name = "GetData")]
public IActionResult GetData(int id)
{
    var key = GetKey(Request.Headers);
    if (key == null)
        return KeyMissingResult();
    var user = GetUser(key);
    if (user == null)
        return InvalidKeyResult();
    if (!user.IsActive)
        return UserInactiveResult(user);
    if (!user.HasRole("data-getter"))
        return UserMissingRoleResult(user.Name, "data-getter");
    var data = GetData(id);
    if (data == null)
        return EntityNotFoundResult($"Could not find data with id {id}");
    return OkResult(data);
}
```

A little bit of test coverage and we got something that works like a charm.

What's the harm, the alternative and how does it reduce said harm?

### The harm
#### Duplication
Since we are used to imperative programming, (remember that this library takes for granted that you come from imperative and want to move to declarative, if you don't, just move along, functional programming is blissful to use but a torture to learn, save yourself the latter if you're not looking for the former), consuming the output of each of those methods and doing checks on it does not feel like code duplication.

**It is duplication, though.**

Since you would have to do these checks in every endpoint that has to handle these cases, well, you are writing the same code over and over again.

"I would do the security checks in middlewares" I hear you say, which leads us to the next problem:

#### Lack of cohesion
You can avoid these kind of checks in every endpoint by using middlewares like ASP.NET does, yes, but that excludes the check for the data not being there, and now you have two error handling scenarios in two different places. Plus, what happens if there's another error, your specific user not being able to access that specific record, like in tenancy or ownership of data scenarios? Just another conditional.

### The alternative
The functional option requires some setup.
```cs
// Explicitly typing our errors:
public class MyError
{
    public readonly Du5<KeyMissing, KeyInvalid, UserInactive, PermissionMissing, EntityNotFound> Value;
    public MyError(Du5<KeyMissing, KeyInvalid, UserInactive, PermissionMissing, EntityNotFound> value) =>
        Value = value;

    public static MyError KeyMissing => new(new KeyMissing());
    public static MyError KeyInvalid => new(new KeyInvalid());
    public static MyError UserInactive(string username) => new(new UserInactive(username));
    public static MyError PermissionMissing(string username, string role) => new(new PermissionMissing(username, role));
    public static MyError EntityNotFound(string message) => new(new EntityNotFound(message));
}

public class KeyMissing { }
public class KeyInvalid { }

public class UserInactive
{
    public UserInactive(string username) =>
        Username = username;
    public string Username { get; }
}
public class PermissionMissing
{
    public PermissionMissing(string username, string role)
    {
        Username = username;
        Role = role;
    }
    public string Username { get; }
    public string Role { get; }
}

public class EntityNotFound
{
    public EntityNotFound(string message) =>
        Message = message;
    public string Message { get; }
}

public class ErrorResult
{
    public ErrorResult(string message) =>
        Message = message;

    public string Message { get; }
}

public class BaseController : ControllerBase
{
    // Single point handling all exit conditions.
    protected IActionResult Handle(MyError error) =>
        error.Value.Match<IActionResult>(
            _ => Unauthorized(new ErrorResult("Missing key in the headers")),
            _ => Unauthorized(new ErrorResult("Key was not recognized")),
            inactive => Unauthorized(new ErrorResult($"User {inactive.Username} is inactive")),
            mising => Unauthorized(new ErrorResult($"User {mising.Username} is missing permission {mising.Role}")),
            notFound => NotFound(new ErrorResult(notFound.Message))
        );
}
```
That's a bit of setup, I know, but it's all the error halding code we'll ever need in our API, and extending it will be done exclusively here.

Now, for the imperative examples it was not worth it to show the logic of the methods, but for the imperative version:
```cs
public Result<string, MyError> GetKey(IDictionary<string, StringValues> dict) =>
    dict.ContainsKey("api-key") ? dict["api-key"].ToString() : MyError.KeyMissing;

public Result<User, MyError> GetActiveUser(string key)
{
    User? user = GetUser(key);
    if (user == null)
        return MyError.KeyInvalid;
    return user.IsActive ? user : MyError.UserInactive(user.Username);
}

public Result<Unit, MyError> HasRole(User2 user, string role) =>
    user.HasRole(role) ? unit : MyError.PermissionMissing(user.Username, role);

public Result<MyData, MyError> GetMyData(int id)
{
    MyData? data = Fetch(id);
    return data != null ? MyError.EntityNotFound($"Could not find data with id {id}") : data;
}
```

Now, the grand finale, the shape that every one of our endpoints will have with this setup.
```cs
[HttpGet(Name = "GetData")]
public IActionResult GetData(int id) =>
(
    from key in GetKey(Request.Headers)
    from user in GetActiveUser(key)
    from isAuthorized in HasRole(user, "data-getter")
    from data in GetMyData(id)
    select data
).Match(Ok, Handle);
```

The type system is enforcing error handling in every step. If you were to check for `isAuthorized` after you get the data, it would still return an error. It can be made safer by having `HasRole` return a token and make it a parameter of `GetMyData`, but it's out of the scope for this example.

More importantly, we have a single point where we translate all possible errors to action results. If we had operations with idempotency keys, we could validate the idempotency key and shortcircuit the whole operation with a cached result with trivial modifications.