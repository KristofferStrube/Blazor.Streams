using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#readablestreambyobreader">Streams browser specs</see>
/// </summary>
public class ReadableStreamBYOBReader : ReadableStreamReader
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStreamBYOBReader"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamBYOBReader"/>.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableStreamBYOBReader"/>.</returns>
    public static ReadableStreamBYOBReader Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return new ReadableStreamBYOBReader(jSRuntime, jSReference);
    }

    /// <summary>
    /// Constructs a <see cref="ReadableStreamDefaultReader"/> from some <see cref="ReadableStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="stream">A <see cref="ReadableStream"/> wrapper instance.</param>
    /// <returns></returns>
    public static async Task<ReadableStreamBYOBReader> CreateAsync(IJSRuntime jSRuntime, ReadableStream stream)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructReadableStreamBYOBReader", stream.JSReference);
        return new ReadableStreamBYOBReader(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS instance of a <see cref="ReadableStreamBYOBReader"/>.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamBYOBReader"/>.</param>
    public ReadableStreamBYOBReader(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// Reads a chunk of a stream.
    /// </summary>
    /// <returns>The next chunk of the underlying <see cref="ReadableStream"/>.</returns>
    public async Task<ReadableStreamReadResult> ReadAsync(ArrayBufferView view)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("read", view);
        return new ReadableStreamReadResult(JSRuntime, jSInstance);
    }
}
