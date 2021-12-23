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
#### Ducplication
Since we are used to imperative programming, (remember that this library takes for granted that you come from imperative and want to move to declarative, if you don't, just move along, functional programming is blissful to use but a torture to learn, save yourself the latter if you're not looking for the former), consuming the output of each of those methods and doing checks on it does not feel like code duplication.

**It is duplication, though.**

Since you would have to do these checks in every endpoint that has to handle these cases, well, you are writing the same code over and over again.

"I would do the security checks in middlewares" I hear you say, which leads us to the next problem:

#### Lack of cohesion
You can avoid these kind of checks in every endpoint by using middlewares like ASP.NET does, yes, but that excludes the check for the data not being there, and now you have two error handling scenarios in two different places. Plus, what happens if there's another error, your specific user not being able to access that specific record, like in tenancy or ownership of data scenarios? Just another conditional.

### The alternative
The functonal option requires some setup.
```cs
public record KeyInvalid;
public record UserInactive(string Username);
public record PermissionMising(string Username, string Permission);
public record EntityNotFound(string Message);

public record MyError(Du4<KeyInvalid, UserInactive, PermissionMising, EntityNotFound> Value);
```