[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](/LICENSE.md)
[![GitHub issues](https://img.shields.io/github/issues/KristofferStrube/Blazor.Streams)](https://github.com/KristofferStrube/Blazor.Streams/issues)
[![GitHub forks](https://img.shields.io/github/forks/KristofferStrube/Blazor.Streams)](https://github.com/KristofferStrube/Blazor.Streams/network/members)
[![GitHub stars](https://img.shields.io/github/stars/KristofferStrube/Blazor.Streams)](https://github.com/KristofferStrube/Blazor.Streams/stargazers)

<!--[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/KristofferStrube.Blazor.Streams?label=NuGet%20Downloads)](https://www.nuget.org/packages/KristofferStrube.Blazor.Streams/)  -->

# Introduction
A Blazor wrapper for the browser API [Streams](https://streams.spec.whatwg.org/)

The API standardizes ways to create, compose, and consume streams of data that map to low-level I/O primitives in the browser. This wrapper tries to implement a wrapper around the API for Blazor so that we can easily and safely interact with the streams of the browser.

<!--## Demo
The sample project can be demoed at https://kristofferstrube.github.io/Blazor.Streams/

On each page you can find the corresponding code for the example in the top right corner.

On the *API Coverage Status* page you can get an overview over what parts of the API we support currently-->

# Getting Started
The package can be used in Blazor projects.
## Prerequisites
You need to install .NET 6.0 or newer to use the library.

[Download .NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)

<!--## Installation
You can install the package via Nuget with the Package Manager in your IDE or alternatively using the command line:
```bash
dotnet add package KristofferStrube.Blazor.Streams
```-->

## Import
You need to reference the package in order to use it in your pages. This can be done in `_Import.razor` by adding the following.
```razor
@using KristofferStrube.Blazor.Streams
```
## Add to service collection
An easy way to make the service available in all your pages is by registering it in the `IServiceCollection` so that it can be dependency injected in the pages and components that need it. This is done in `Program.cs` by adding the following before you build the host and run it.
```csharp
var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Other services are added.

builder.Services.AddStreamsService();

await builder.Build().RunAsync();
```
## Inject in page
Then the service can be injected in a page like so:
```razor
@inject StreamsService StreamsService;
```
Then you can use `StreamsService` to create a wrapper instance around either a `ReadableStream`, `WritableStream`, or `TransformStream`. The difference between these are described later in this README.

**TODO: Sample of use**

# Nomenclature
## ReadableStream
## WritableStream
## TransformStream

# Issues
Feel free to open issues on the repository if you find any errors with the package or have wishes for features.

# Related articles
This repository was build with inspiration and help from the following series of articles:

- [Wrapping JavaScript libraries in Blazor WebAssembly/WASM](https://blog.elmah.io/wrapping-javascript-libraries-in-blazor-webassembly-wasm/)
- [Call anonymous C# functions from JS in Blazor WASM](https://blog.elmah.io/call-anonymous-c-functions-from-js-in-blazor-wasm/)
- [Using JS Object References in Blazor WASM to wrap JS libraries](https://blog.elmah.io/using-js-object-references-in-blazor-wasm-to-wrap-js-libraries/)
- [Blazor WASM 404 error and fix for GitHub Pages](https://blog.elmah.io/blazor-wasm-404-error-and-fix-for-github-pages/)
- [How to fix Blazor WASM base path problems](https://blog.elmah.io/how-to-fix-blazor-wasm-base-path-problems/)
