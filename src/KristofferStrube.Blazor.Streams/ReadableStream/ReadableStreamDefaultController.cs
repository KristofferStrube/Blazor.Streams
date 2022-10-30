using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#readablestreamdefaultcontroller">Streams browser specs</see>
/// </summary>
public class ReadableStreamDefaultController : ReadableStreamController
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStreamDefaultController"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamDefaultController"/>.</param>
    public ReadableStreamDefaultController(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// Enqueues the chunk in the controlled stream.
    /// </summary>
    /// <param name="chunk"></param>
    /// <returns></returns>
    public async Task EnqueueAsync(IJSObjectReference? chunk = null)
    {
        await JSReference.InvokeVoidAsync("enqueue", chunk);
    }
}
