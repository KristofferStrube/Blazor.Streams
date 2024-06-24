using KristofferStrube.Blazor.FileAPI.Options;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

internal static class IJSRuntimeExtensions
{
    internal static async Task<IJSObjectReference> GetHelperAsync(this IJSRuntime jSRuntime, StreamsOptions? options = null)
    {
        options ??= StreamsOptions.DefaultInstance;
        return await jSRuntime.InvokeAsync<IJSObjectReference>(
            "import", options.FullScriptPath);
    }
    internal static async Task<IJSInProcessObjectReference> GetInProcessHelperAsync(this IJSRuntime jSRuntime, StreamsOptions? options = null)
    {
        options ??= StreamsOptions.DefaultInstance;
        return await jSRuntime.InvokeAsync<IJSInProcessObjectReference>(
            "import", options.FullScriptPath);
    }
}
