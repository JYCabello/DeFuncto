# Fetch, filter, transform, iterate
The four core tenets of information systems. Pretty much every business process you implement can be represented by these four steps. Sometimes you can skip filtering, transform or both of them, but no individual process can avoid fetching and iterating and no system as a whole will prove to solve a real world problem without filtering and transforming.

> Ha!, classic database mentality. What about this endpoint in my API that receives no data and returns the current time?

Such a system needs to listen on a port, where it will **fetch** data from the network layer for every request received. Then it would **iterate** over those bits to get the route. After that, process the route, **filtering** out irrelevant ones and **transforming** the route in a method call that will **fetch** the current time from the sytem clock, then it could apply some format **transforming** it into text or just keeping it as a timestamp that has to be **iterated** over to serialize it into an http response.

> The last **iterate** part is a bit far fetched...

It is, save that thought for the last bit. Every step will be explained in detail and the iterate one is the least intuitive of the lot if you are heavily object oriented. On the meanwhile, **bear with me**.
```
                __
     __/~~\-''-/_ |
__- - {            \
     /             \
    /       ;o    o }
    |_             ;
      \            '
       \_       (..)
         ''-_ _ _ /
           /
          /
```

## Fetch
Getting data from the outside world into the system. Every example of information processing starts with this step. Parsing user input in a form or a CLI, processing an HTTP request in your server, making a query to your database or reading the input from an humidity sensor. In general, anything that can fail in a non-deterministic fashion and gives you data on success.

In C#, we usually wrap those in the class `Task<TypeToFetch>`, this is mainly meant to achieve [asynchrony](asynchrony.md), but it also represents a request that can go wrong for reasons not related to our logic.

From the pragmatic point of view, we do some of these operations as if they were deterministic, like requesting the current system time and move along with our lives.
## Filter
Filtering is a deterministic operation we do on data, it consists on making a decision of how or even if we are going to process it.

This is done in [imperative code](imperative.md) by means of conditionals, that will branch our execution path depending on certain checks. When done [declaratively](declarative.md), this is done by wrapping our piece of information in a data structure that represents the path we want to take.
## Transform
A deterministic operation where we take an input and return an output. Adding two numbers, formatting a string or mapping a model are examples of it.

When working [imperatively](imperative.md) this is often done by mutating values or pointers of existing designations, when working [declaratively](declarative.md) and in many [imperative](imperative.md) cases, this is achieved by creating a new value representing the outcome of the operation, keeping the original reference intact and often even discarding it.
## Iterate
A non-deterministic operation where we execute an action over data. Saving to any form of storage, returning the response to an API call or writing in a console.

Bear in mind that it's easy to mix it up with iterating over a collection to filter and/or transform in [imperative](imperative.md) code, iterating over a list to render table rows in html is not really iterating in this definition, but writing the whole html to the http response is (or altering the DOM to introduce those rows in a client side scenario).

### Use of these definitions
A big win coming from Functional Programming is that you get to write functions with one and only one concern. It's taking single responsibility to the extreme. Using these definitions will help you in this endeavor. Try to never fetch and transform or filter and iterate in the same function and you will be one step closer to write the most decoupled code you have ever written. I will not defend you from doing two different transformations in one go, but quality in software development is pretty much about compounding practices. Every bit you do, will multiply the outcome.