using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#rbs-controller-class">Streams browser specs</see>
/// </summary>
public class ReadableByteStreamController : ReadableStreamController
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableByteStreamController"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableByteStreamController"/>.</param>
    public ReadableByteStreamController(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// Returns the current BYOB pull request, or null if there isn’t one.
    /// </summary>
    /// <returns>A <see cref="ReadableStreamBYOBRequest"/></returns>
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

    /// <summary>
    /// Enqueues the chunk in the controlled stream.
    /// </summary>
    /// <param name="chunk"></param>
    /// <returns></returns>
    public async Task EnqueueAsync(ArrayBufferView chunk)
    {
        await JSReference.InvokeVoidAsync("enqueue", chunk.JSReference);
    }
}
