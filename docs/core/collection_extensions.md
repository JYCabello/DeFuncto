# Collection extensions
Helpers to let you handle collections in a functional way.

There wasn't a lot of work to do there, as collection processing is one of the aspects of functional programming that almost every modern software developer does in a functional way. LINQ is functional programming in both its fluent and embbedded syntax.

Still, there were a few minor things that would be common enough to address them in this library:

## XOrNone
Wrappers of `SingleOrDefault` and `FirstOrDefault` that will give you `None` in the situations where you would get `default` (internally boxes values in order to prevent returning `Some({default value})` if you were to do it in a of structs/primitives with a default value).
```cs
var eight = new List<int> { 8, 3, 89 }.FirstOrNone(); // Some(8)
var noneBecauseEmpty = new List<int> { }.FirstOrNone(); // None
var twenty = new List<int> { 20 }.SingleOrNone(); // Some(20)
var noneBecauseMany = new List<int> { 13, 5, 8 }.SingleOrNone(); // None
```

## Choose
Related with [`Option`](./option.md).

Choose offers us the possibility of constructing inline collections with optional elements or even doing filtering and transformations in a "single step" (not really, as the filtering happens at the type level while the transformation is explicit). Internally, it just takes an inumerable of `Option<T>` and returns an `IEnumerable` of `T` for all options that were in the `Some` state.
### Optional construction
```cs
using static DeFuncto.Prelude;
//...

// This will return an enumerable with:
// a, if it was a multiple of three
// b, if it was greater than 200
// c, if it was smaller than 75
public IEnumerable<int> OptionalConstruction(int a, int b, int c) =>
    new List<Option<int>>
    {
        a % 3 == 0 : Some(a) : None,
        b > 200 : Some(b) : None,
        c < 75 : Some(c) : None
    }.Choose();
```
### Filter and transform in one go

```cs
using static DeFuncto.Prelude;
//...

// This will give a collection of all integers that could be parsed out of the input strings.
public IEnumerable<int> FilterAndTransform(IEnumerable<string> input) =>
    inputs.Choose(s => int.TryParse(s, out var result) : Some(result) : None);

```

## Task collection elevation
These extensions "elevate" the collection inside a task so we can `Select`, `ToList` and `ToArray` them to get another collection in a task.

```cs
var myAsyncArray =
    Task.FromResult(new[] { 1, 2, 3 }) // Task<int[]> { 1, 2, 3 }
        .Select(n => n * 2) // Task<IEnumerable<int>> { 2, 4, 6 }
        .ToArray(); //                    Task<int[]> { 2, 4, 6 }
```