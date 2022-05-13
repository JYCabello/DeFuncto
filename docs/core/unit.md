# Unit (and nothing)
How many possible outcomes can this function have?
```cs
public bool IsActive() => //...
```

Easy, isn't it?

What about [this one](https://thedailywtf.com/articles/what_is_truth_0x3f_)?

```cs
public enum Bool
{
    True,
    False,
    FileNotFound
}

public Bool WhatIsTruth() => //...
```

Right, you're on a roll.

How many from this one?

```cs
public void DoSometing() => //...
```

You got that one right too? If you answered 3, 4 and 2, and I have no reason to think otherwise, you're correct, brilliant!

As for the possible outcomes of `IsActive`, we got `true`, `false` and `nothing`.

`nothing` as in:
1. Throwing an exception.
1. Pulling the plug of the computer.
1. Lightning strike.
1. Entering an infinite loop (although that would probably throw an exception).
1. The system awaits for a remote response that never comes and does not have a timeout mechanism, getting stuck forever.

That concept of nothing has a [mathematical name](https://en.wikipedia.org/wiki/Bottom_type), but we're not using it here.

If you have accepted this as true, or at least you have agreed to entertain the idea, the outcomes of `WhatIsTruth` would be `True`, `False`, `FileNotFound` and `nothing`. No surprises here.

What about `DoSomething`? Obviously `nothing` and... well, `something`.

How do we represent this `something`? To explain it, we have to talk about math, just a little bit, just one tiny little thing.

> In math, all functions take one parameter and return one value. Functional programming is the illegitimate child of math and computers, so it follows this principle.

You might be rightfully thinking that you kind of remember having functions taking multiple parameters, even in F# or Haskell, you have seen functions take more than one parameter.

```fs
// F#
let add a b = a + b
// The type of this function is:
// int -> int -> int
```
```haskell
-- Haskell
add a b = a + b
-- The type of this function is:
-- Int -> Int -> Int
```
```cs
// C#
public int Add(int a, int b) => a + b;
// The type of this function is:
// Func<int, int, int>
```

The types of F# and Haskell clearly follow this rule. In both cases, `add` is a function that takes an integer and returns a function that takes an integer and returns an integer.

Read that again.

It's a function that takes one parameter, and returns one value. That value happens to be another function that takes one parameter and returns a value.

As for C#: What's the type of a two-integer tuple? Exactly `(int, int)`. How do you write a tuple of the numbers 1 and 2? Right `(1, 2)`. So, when you want to add 1 and 2, you call add with said tuple `Add(1, 2)`.

You might be wondering?
1. What?
1. How does this relate to `void`?
1. What?

For the first and third point, may I present you how the F# version looks like when using a tuple?
```fs
let add(a, b) = a + b
// The type of this function is:
// (int, int) -> int
// And it would be called like this:
let seven = add(3, 4)
```

As for the second: If every function has to take a parameter, what happens when we get a function like this:
```cs
public int GetNumber() => 7;
```

This is the story, `GetNumber` **does** get a parameter, because:
```cs
// This
int withParameter = GetNumber();
// And this
Func<int> withoutParameter = GetNumber;
// Are only different in terms of what we are passing to GetNumber.
```

And *what* are we passing to the function in the second case? Nothing.

And for the first case? Well... `something`.

The same `something` as before, it's a thing that can have only one possible value, its very existence.

Now imagine a tuple of zero elements. That has no range of values, it just **is**. Which can be written as `()`.

This concept, a space state that has only one possible case, is conveniently called `unit` in most functional programming languages, and is written as `()`. That's the case for Haskell and F#.

Why does this matter?

In functional programming we often chain functions and use expressions to compose data structures. All of those structures need to return something to make those comprehensions. So, in DeFuncto, you would return `Unit` anywhere you had a `void` function and `Task<Unit>` anywhere you had `Task`.

It's midly annoying, but it's the price to pay for composability.
