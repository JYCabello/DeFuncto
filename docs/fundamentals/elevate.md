# Elevate
When working with data structures, is not uncommon to see yourself mixing and macthing, from a simple scenario when you have a guaranteed value in a function that is meant to return [option](../core/option.md) to more complex scenarios where we have to return a [result](../core/result.md) while having one function that returns a guaranteed value, another that returns a nullable value and one result with a different error type.

## The problem, by example
### Simple case, solved out of the box
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
    (
        from delay in GetDelay()
        from retries in GetRetries()
        select new Settings
        {
            Delay = delay,
            Retries = retries,
            ThrottleRate = GeThrottleRates()
        }
    );
}
```
Quite simple, our expression will bind the delay and the retries, but there's nothing needed to do about the throttle rate, so we just get the value.

What happens if we had a list of throttle rates and we wanted to get the first element or return `None` if no values were found? There's already a solution for that, `FirstOrNone` which will elevate our list to an option. Think of an option like a list that can have one or no elements, so we are just reducing all the "one or more elements" cases of a list to the "one element" case in the option and converting the empty case to "None".

