# Fetch, filter, transform, iterate
The four core tenets of information systems. Pretty much every business process you implement can be represented by these four steps. Sometimes you can skip filtering, transform or both of them, but no individual process can avoid fetching and iterating and no system as a whole will prove to solve a real world problem without filtering and transforming.

> Ha!, classic database mentality. What about this endpoint in my API that receives no data and returns the current time?

Such a system needs to listen on a port, where it will **fetch** data from the network layer for every request received. Then it would **iterate** over those bits to get the route. After that, process the route, **filtering** out irrelevant ones and **transforming** the route in a method call that will **fetch** the current time from the sytem clock, then it could apply some format **transforming** it into text or just keeping it as a timestamp that has to be **iterated** over to serialize it into an http response.

> The last **iterate** part is a bit far fetched...

It is, save that thought for the last bit. Every step will be explained in detail and the iterate one is the least intuitive of the lot if you are heavily object oriented. On the meanwhile, **bear with me**.
```
                __
     __/~~\-''- _ |
__- - {            \
     /             \
    /       ;o    o }
    |              ;
                   '
       \_       (..)
         ''-_ _ _ /
           /
          /
```

## Fetch
Getting data from the outside world into the system. Every information processing starts with this step.

