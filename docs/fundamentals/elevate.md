# Elevate
When working with data structures, is not uncommon to see yourself mixing and macthing, from a simple scenario when you have a guaranteed value in a function that is meant to return [option](../core/option.md) to more complex scenarios where we have to return a [result](../core/result.md) while having one function that returns a guaranteed value, another that returns a nullable value and one result with a different error type.

## The problem, by example
### From option to granular error
We have a function returning `Option<Settings>` which requires two values that are obtained form optional sources and one that is guaranteed to be there:
```cs
public class Settings
{
    public int Delay { get; set; }
    public int Retries { get; set; }
    public int ThrottleRate { get; set; }
}

public interface ISettingsService
{
    Option<Settings> Get();
}

public class SettingsService : ISettingsService
{
    public static Option<int> GetDelay() => /* ... */;
    public static Option<int> GetRetries() => /* ... */;
    public static int GetThrottleRate() => /* ... */;

    Option<Settings> ISettingsService.Get() =>
        from delay in GetDelay()
        from retries in GetRetries()
        select new Settings
        {
            Delay = delay,
            Retries = retries,
            ThrottleRate = GeThrottleRate()
        };
}
```
Quite simple, our expression will bind the delay and the retries, but there's nothing needed to do about the throttle rate, so we just get the value.

But what would happen if, in our use case, we wanted to be granular with our errors. We are not just interested in the settings being there or not, but on the reason why it was absent. We have already covered what [Result](../core/result.md) can do in this sense, so the simplest solution would be to just change the return types of `GetDelay` and `GetRetries`, but often we will not have that option or it simply doesn't make sense, since it might be a fuction that just returns an `Option` in its context, and the meaning of `None` changes dramatically depending on the context where said function is being consumed.

In said context, we will have to change our signature to one that illustrates our domain concerns:
```cs
public class DelayMissing { }
public class RetriesMissing { }

public class SettingsError
{
    private readonly Du<DelayMissing, RetriesMissing> value;

    public SettingsError(Du<DelayMissing, RetriesMissing> value) =>
        this.value = value;

    public T Match<T>(Func<DelayMissing, T> onDelayMissing, Func<RetriesMissing, T> onRetriesMissing) =>
        value.Match(onDelayMissing, onRetriesMissing);

    public static readonly SettingsError DelayMissing = new(new DelayMissing());
    public static readonly SettingsError RetriesMissing = new(new RetriesMissing());
}

public interface ISettingsService
{
    Result<Settings, SettingsError> Get();
}
```

Now that we have defined our error types, we just need to translate our `None` cases to the relevant error:
```cs
public class SettingsService : ISettingsService
{
    public static Option<int> GetDelay() => /* ... */;
    public static Option<int> GetRetries() => /* ... */;
    public static int GetThrottleRate() => /* ... */;

    Result<Settings, SettingsError> ISettingsService.Get() =>
        from delay in GetDelay().Result(SettingsError.DelayMissing)
        from retries in GetRetries().Result(SettingsError.DelayMissing)
        select new Settings
        {
            Delay = delay,
            Retries = retries,
            ThrottleRate = GetThrottleRate()
        };
}
```

This is a way to click any pieces together, every function can return any data structure and you just need to "translate" the cases that do not match your use case.