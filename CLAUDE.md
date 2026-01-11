# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Build and Test Commands

```bash
# Build the solution
dotnet build ./src/DeFuncto.sln

# Build in release mode
dotnet build -c Release ./src/DeFuncto.Core/DeFuncto.Core.csproj

# Run all tests
dotnet test ./src/DeFuncto.sln

# Run a single test file (example)
dotnet test ./src/DeFuncto.Tests --filter "FullyQualifiedName~DeFuncto.Tests.Core.Types.Option.Linq"

# Run a specific test method
dotnet test ./src/DeFuncto.Tests --filter "DisplayName~Binds all somes"

# Create NuGet package
dotnet pack -c Release -o . ./src/DeFuncto.Core/DeFuncto.Core.csproj
```

## Architecture Overview

DeFuncto is a functional programming library for C# inspired by F# and language-ext. It provides discriminated union types for railway-oriented programming.

### Project Structure

- **DeFuncto.Core** (netstandard2.0): Main library with core types
- **DeFuncto.Assertions** (netstandard2.0): Test assertion helpers for DeFuncto types
- **DeFuncto.Tests** (net9.0): xUnit + FsCheck property-based tests

### Core Types (in `src/DeFuncto.Core/Types/`)

| Type | Purpose |
|------|---------|
| `Option<T>` | Value that may be absent (Some/None) |
| `Result<TOk, TError>` | Success/failure result (Ok/Error) |
| `AsyncOption<T>` | Async version of Option wrapping `Task<Option<T>>` |
| `AsyncResult<TOk, TError>` | Async version of Result wrapping `Task<Result<TOk, TError>>` |
| `Du<T1, T2>` through `Du<T1...T7>` | Unbiased discriminated unions with 2-7 cases |
| `Unit` | Void replacement for functional composition |

### Key Design Patterns

**Prelude static class**: Import `static DeFuncto.Prelude` for factory functions:
- `Some(value)`, `None` - Option construction
- `Ok(value)`, `Error(value)` - Result construction
- `Id<T>()` - Identity function
- `unit` - Unit singleton

**LINQ query syntax**: Types support `Select`/`SelectMany`/`Where` for composition:
```csharp
from user in userService.FindUser(name)
from auth in cryptoService.Validate(user, password)
from token in sessionService.CreateToken(auth)
select token;
```

**Async bridging**: Call `.Async()` to lift sync types to async versions:
```csharp
Result<User, Error>.Ok(user).Async()  // → AsyncResult<User, Error>
```

### Extensions (`src/DeFuncto.Core/Extensions/`)

Utility extensions for common operations:
- `Objects.cs`: `Apply()` for pipeline composition
- `Tasks.cs`: Task mapping, parallel execution
- `Collections.cs`: `FirstOrNone()`, `SingleOrNone()`, `Choose()`
- `Functions.cs`: `Compose()`, action-to-function conversion

### Testing Conventions

- Uses FsCheck for property-based testing with `[Property]` attribute
- Assertion methods: `ShouldBeSome()`, `ShouldBeNone()`, `ShouldBeOk()`, `ShouldBeError()`
- `Witness` and `ConcurrentWitness` helpers track side effects in tests

### Compiler Settings

- Nullable reference types enabled with `WarningsAsErrors=nullable`
- All types are readonly structs for value semantics
- Methods use `[Pure]` and `[MethodImpl(AggressiveInlining)]` attributes
