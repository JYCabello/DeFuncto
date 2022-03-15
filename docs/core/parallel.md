# Parallel

Helper function that allows the parallelization of a collection of operations, gathering all the outputs in a collection. Has a maximum degree of parallelism of five by default, meaning that, regardless of the size of the collection, only five operations will be executed a the same time.

Tasks in .NET are what is known as a `hot promise`, a structure that wraps the promise of a future value, but that starts executing as soon as they are constructed.

In order to be able to parallelize, we need to delay the execution, we do this by wrapping the task in a `Func<Task<T>>`, which is just a function that triggers the task. It is important that the task is triggered inside that function, or it will start executing before the latch mechanisims in `Parallel` can limit the concurrent executions. This will efectively will turn our `Task` into a `cold promise`, one that will not start running until invoked.

There is an optional parameter (an integer) to define a different degree of parallelism.
```cs
public class User
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
}

public async Task<User> GetUser(int id) =>
    // Request to an API that only allows us to query one user at a time...

public Task<User[]> GetUsers(IEnumerable<int> userIds)
{
    Func<Task<User>> GetUserCold(int userId) =>
        async () => await GetUser(userId);

    return userIds
        .Select(GetUserCold)
        .Parallel();
}

// GetUserCold is valid because you need to first pass a parameter to get a function.
// Said function can be invoked in order to get a task, which is already running.
// Awaiting said task will give you the final value.
Func<Task<User>> user7Cold = GetUserCold(5);
Task<User> user7Task = user7Cold();
User user7 = await user7Task;
```
