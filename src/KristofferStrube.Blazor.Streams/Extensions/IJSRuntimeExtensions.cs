using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams.Extensions;

internal static class IJSRuntimeExtensions
{
    internal static async Task<IJSObjectReference> GetHelperAsync(this IJSRuntime jSRuntime)
    {
        return await jSRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/KristofferStrube.Blazor.Streams/KristofferStrube.Blazor.Streams.js");
    }
    internal static IJSInProcessObjectReference GetInProcessHelper(this IJSRuntime jSRuntime)
    {
        return ((IJSInProcessRuntime)jSRuntime).Invoke<IJSInProcessObjectReference>(
            "import", "./_content/KristofferStrube.Blazor.Streams/KristofferStrube.Blazor.Streams.js");
    }
}
