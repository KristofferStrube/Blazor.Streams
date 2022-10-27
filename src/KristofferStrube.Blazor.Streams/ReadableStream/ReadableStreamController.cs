using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#typedefdef-readablestreamcontroller">Streams browser specs</see>
/// </summary>
public abstract class ReadableStreamController : BaseJSWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStreamController"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamController"/>.</param>
    internal ReadableStreamController(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task<double?> GetDesiredSizeAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<double?>("getAttribute", JSReference, "desiredSize");
    }

    public async Task CloseAsync()
    {
        await JSReference.InvokeVoidAsync("close");
    }

    public async Task ErrorAsync()
    {
        await JSReference.InvokeVoidAsync("error");
    }
}
