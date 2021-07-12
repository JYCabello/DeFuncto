# DeFuncto
Functional library for C#, aiming to keep the minimum data types for ease of maintenance.

Heavily inspired by [language-ext](https://github.com/louthy/language-ext), but keeping it closer to F# most used structures and with a much less ambitious scope, making it easier to keep it up to date with the latest versions of the language.

The approach is to let C# developers to dip a toe in the pool of programming in a safer way. It's meant in a pragmatic and simplistic way and nothing mentioned in this documents should be considered a computer science compendium on how to write mathematically correct software, but a cookbook of recipes to move you closer to that.

> Wait, mathematically correct software? Is that even possible?

Indeed, you can write software in a way where invalid states are impossible to represent. Most of the mistakes you can make will just not compile.

> So, I can achieve this "correctness" thing with DeFuncto?

No. The data types defined here are structs, which by default have an empty constructor (C# does not allow to remove it) that will, with the notable exception of `Option` and `AsyncOption` (where it's done in a bit of a hacky way) screw any hope of correctness.

There IS a way around it, and [language-ext](https://github.com/louthy/language-ext) does it by implementing `_|_`

## Functional Programming as a concept: Making your little dev world safer
I'm taking

## Documentation
1. [DeFuncto.Core](docs/core/index.md)
