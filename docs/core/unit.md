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
```cs
public int Add(int a, int b) => a + b;
```
```fs
let add a b = a + b
```
```haskell
add a b = a + b
```