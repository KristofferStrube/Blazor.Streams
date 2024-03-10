using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#rbs-controller-class">Streams browser specs</see>
/// </summary>
public class ReadableByteStreamController : ReadableStreamController, IJSCreatable<ReadableByteStreamController>
{
    /// <inheritdoc/>
    public static async Task<ReadableByteStreamController> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static Task<ReadableByteStreamController> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new ReadableByteStreamController(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected internal ReadableByteStreamController(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }

    /// <summary>
    /// Returns the current BYOB pull request, or null if there isn’t one.
    /// </summary>
    /// <returns>A <see cref="ReadableStreamBYOBRequest"/></returns>
    public async Task<ReadableStreamBYOBRequest?> GetBYOBRequestAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference? jSInstance = await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "byobRequest");
        if (jSInstance is null)
        {
            return null;
        }
        return new ReadableStreamBYOBRequest(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Enqueues the chunk in the controlled stream.
    /// </summary>
    /// <param name="chunk">An <see cref="IArrayBufferView"/> supplied as the BYOB.</param>
    public async Task EnqueueAsync(IArrayBufferView chunk)
    {
        await JSReference.InvokeVoidAsync("enqueue", chunk.JSReference);
    }
}
