# Asynchrony
When fetching data from outside the memory space assigned to your program, you often face an `IO` delay. That is, waiting for a system service to provide you an response for something you requested. You might have made a database query, made an http request or asked the system to read from a file.

These operations are usually orders of magnitude slower than the ones you execute directly in memory and they have no use for your precious processor time. For this reason, many languages take a promise based approach to `IO` operations.

.NET is able to suspend the current thread, releasing its resources to be effectively used, when waiting for an `IO` operation to respond, once the response is there, the `Task` is resumed, assiging it a new thread that behaves just like the previous one.

Should we have only one processor available, and one set of 5 parallel operations consisting of 10 milliseconds of processing and 90 of waiting for `IO`, we could run the whole operation in 140 milliseconds instead of 500 if we were to block the current thread until the operation has finished.