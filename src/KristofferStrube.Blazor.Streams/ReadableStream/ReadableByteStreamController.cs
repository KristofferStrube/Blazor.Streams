using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#readablestreamdefaultcontroller">Streams browser specs</see>
/// </summary>
public class ReadableByteStreamController : ReadableStreamController
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableByteStreamController"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableByteStreamController"/>.</param>
    public ReadableByteStreamController(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task<ReadableStreamBYOBRequest?> GetBYOBRequestAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        var jSInstance = await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "byobRequest");
        if (jSInstance is null)
        {
            return null;
        }
        return new ReadableStreamBYOBRequest(jSRuntime, jSInstance);
    }

    public async Task EnqueueAsync(ArrayBufferView chunk)
    {
        await JSReference.InvokeVoidAsync("enqueue", chunk.JSReference);
    }
}
