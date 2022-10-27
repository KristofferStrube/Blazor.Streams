using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#readablestreambyobrequest">Streams browser specs</see>
/// </summary>
public class ReadableStreamBYOBRequest : BaseJSWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStreamBYOBRequest"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamBYOBRequest"/>.</param>
    internal ReadableStreamBYOBRequest(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task<ArrayBufferView?> GetViewAsync()
    {
        var helper = await helperTask.Value;
        var jSInstance = await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "view");
        if (jSInstance is null)
        {
            return null;
        }
        return new ArrayBufferView(jSInstance);
    }

    public async Task RespondAsync(ulong bytesWritten)
    {
        await JSReference.InvokeVoidAsync("respond", bytesWritten);
    }

    public async Task RespondWithNewViewAsync(ArrayBufferView view)
    {
        await JSReference.InvokeVoidAsync("respondWithNewView", view.JSReference);
    }
}
