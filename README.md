# mcm4csharp
Async MC-Market Ultimate API for C#

# Quickstart
Initialise the client somewhere in your program. You should only use one instance throughout it, unless for some reason you're logging in with different tokens:
```cs
// ...
ApiClient client = new ApiClient(TokenType.Private, "your_private_token");
// ... or ...
ApiClient client = new ApiClient(TokenType.Shared, "your_shared_token");
// ...
```
You will need to ensure you're using the right token type. To do so, refer to [the API documentation](https://www.mc-market.org/wiki/v1-endpoints/).

To ensure you're correctly authenticated, run:
```cs
var response await client.GetHealthAsync();
if (response.Status != "success") {
  // reauthenticate or retry here
}
```

Invoking the wrapper is straightforward. For methods which have a body, you will need to refer to the appropriate struct. IDs and others are passed by parameters.

Each asynchronous method has a return type of `Response<T>`:
```cs
public struct Response<T> {
	public string Result { get; set; }
	public T Data { get; set; }
	public Error Error { get; set; }
	public ulong RetryAfterMilliseconds { get; set; }
}
```
`Result` returns the API-documented result. `Data` contains reponse data of type `T` and `Error` gives any errors listed in the response, where applicable.

Since the API only returns `RetryAfterMilliseconds` when you're being ratelimited, any time that it is 0 you can assume you're not being ratelimited.

For endpoint that returns a result that can be sorted, sortable options are available under `v1.Client.SortableFields`; 
please refer to the xml documentation for information on which field supports which endpoint. You will additionally find defaults there, too.

# Documentation
This project has a Doxyfile availale for you to build documentation.

In the directory `/docs`, run `doxygen` using the `Doxyfile` located in the directory. You will see folders `/html` and `/latex` being built respectively.

To access HTML documentation, run `/docs/html/index.html`. We use `doxygen-awesome` for the theme.
