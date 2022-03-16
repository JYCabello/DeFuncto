# Async wrappers for Option and Result
`Option` and `Result` have asynchronous versions, `AsyncOption` and `AsyncResult`.

They are a prime example of the concept of [elevation](../fundamentals/elevate.md). Since promised values in C# are wrapped in a `Task`, you would often have to `Map` or `Bind` an `Option` to something returning a `Task` or even an `Option` inside a `Task` to something returning a `Task`.
The problem:
```cs
public class User
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class Address
{
    public int UserID { get; set; }
    public string StreetName { get; set; } = string.Empty;
}

public Option<string> GetUserToken() =>
    // Getting the token from some authentication mechanism.
    throw new NotImplementedException();

public Task<Option<User>> GetUser(string token) =>
    // Some database searching.
    throw new NotImplementedException();

public Task<Address> GetMainAddress(int id) =>
    // Address is guaranteed to exist for a given user.
    throw new NotImplementedException();

public async Task<Option<Address>> GetCurrentAddress()
{
    Option<string> userToken = GetUserToken();

    Option<User> user = await userToken
        .Match(
            GetUser, // This is just "token => GetUser(token)", but there's no need to write the outer lambda.
            () => Option<User>.None.ToTask()
        );

    return await user
        .Match(
            u => GetMainAddress(u.ID).Map(Some),
            () => Option<Address>.None.ToTask()
        );
}
```

You can see how this can become complex, specially if you need to combine several `Option` to get a new value.

`AsyncOption` to the rescue:
```cs
public AsyncOption<Address> GetCurrentAddressAsync() =>
    GetUserToken().Async()
        .Bind(GetUser)
        .Map(u => GetMainAddress(u.ID));

public AsyncOption<Address> GetCurrentAddressAsyncLinq() =>
    from token in GetUserToken().Async()
    from user in GetUser(token).Async()
    from address in GetMainAddress(user.ID).Map(Some).Async() // this returns a Task, so we elevate it to AsyncOption
    select address;
```
