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

}

```