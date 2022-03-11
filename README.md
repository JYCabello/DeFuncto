# DeFuncto
Functional library for C#, aiming to keep the minimum data types for ease of maintenance.

Heavily inspired by [language-ext](https://github.com/louthy/language-ext), but keeping it closer to F# most used structures and with a much less ambitious scope, making it easier to keep it up to date with the latest versions of the language.

The approach is to let C# developers to dip a toe in the pool of programming in a safer way. It's meant in a pragmatic and simplistic way and nothing mentioned in this documents should be considered a computer science compendium on how to write mathematically correct software, but a cookbook of recipes to move you closer to that.

> Wait, mathematically correct software? Is that even possible?

Indeed, you can write software in a way where invalid states are impossible to represent. Most of the mistakes you can make will just not compile. There's even an [operating system microkernel](https://www.usenix.org/system/files/login/articles/125-klein.pdf) that is proven to be correct to the last line.

> So, I can achieve this "correctness" thing with DeFuncto?

No. The data types defined here are structs, which by default have an empty constructor (C# does not allow to remove it) that will, with the notable exception of `Option` and `AsyncOption` (where it's done in a bit of a hacky way) screw any hope of correctness.

There IS a way around it, and [language-ext](https://github.com/louthy/language-ext) does it by implementing the concept of `Bottom`, commonly represented as `_|_`. Again, this library is not aiming to get you to swim in the deep end, but to have a smooth transition into safer practices. By keeping the implementation to the minimum, we can maximize the time invested in documentation, sharing the motivations and value of the practices promoted here as well as common pitfalls and tricks to maximize the gains.

> Does this mean that I should just skip this and go straight away to use F#?

If you want and can, yeah, do. This is for those of us that want to make our C# journey a little bit easier.

## Functional Programming as a tool: Making your little dev world safer
The goal is not to make a full introduction to functional programming. When you arrived here I assumed that you already have heard of the word [Monad](https://mikhail.io/2018/07/monads-explained-in-csharp-again/) and/or you might be interested in [railway oriented programming](https://fsharpforfunandprofit.com/rop/). This library aims EXACTLY to give you the tools to do railway oriented programming and nothing else. The idea is that you will end up writing the majority of your programs in a DSL fashion. Defining your work as events that should happen instead of how they happen.

Let's say that your services all return `AsyncResult<Something, Error>`, this is what a login program would look like (for the hardcore FPers: I am aware that this is using old fashioned dependency-injected services) in your session handler:
```cs
public AsyncResult<SessionToken, Error> Login(string name, string password) =>
    from user in userService.FindUser(name)
    from authenticatedUser in cryptoService.Validate(user, password)
    from token in sessionService.TryCreateToken(authenticatedUser)
    select token;
```
> What? That's a query, not a method executing logic.

It's not only executing logic, but doing so asynchronously, this chunk of logic is abstracting away:
- Error handling
- Tasks

The first method called could look something like this:
```cs
public AsyncResult<User, Error> FindUser(string name)
{
    // Some verbosity that you might grow used to, if you don't, you can always make this return
    // Task<Result<User, Error>> and invoke .Async() in the caller.
    return Go().Async();

    async Task<Result<User, Error>> Go()
    {
        var possibleUser = await db.Users.FirstOrDefaultAsync(u => u.Name == name);
        return possibleUser is not null
            ? Result<User, Error>.Ok(user)
            : Result<User, Error>.Error(new Error.EntityNotFound($"User named {name} was not found in the database"));
    }
}
```
Then, the data type takes care of consuming the user in the next method or circuit breaking in case of an error.
> But I don't like magic! I want to see my control flow.

This is not magic, if you change the type returned from `FindUser` this will stop compiling, wether you return something different than `User` on  the `Ok` case or something that is not an `Error` on the `Error` side. The control flow itself happens internally in the data structure just like it happens in the CLI when you do an `if else`, except that this is a "conditional" that you have to work REALLY hard to type wrong and have it compile.

It's all about having the compiler hold your hand for as long as possible. Want to know more? Dive into the docs.


## Documentation
1. [Fundamentals](docs/fundamentals/index.md)
1. [DeFuncto.Core](docs/core/index.md)
1. [Example of a railway oriented API ASP.NET Core 6](https://github.com/JYCabello/fp-railway-asp)
