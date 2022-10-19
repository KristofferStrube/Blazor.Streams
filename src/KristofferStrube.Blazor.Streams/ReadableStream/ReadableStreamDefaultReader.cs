using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#readablestreamdefaultreader">Streams browser specs</see>
/// </summary>
public class ReadableStreamDefaultReader : ReadableStreamReader
{
    /// <summary>
    /// Constructs a <see cref="ReadableStreamDefaultReader"/> from some <see cref="ReadableStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="stream">A <see cref="ReadableStream"/> wrapper instance.</param>
    /// <returns></returns>
    public static async Task<ReadableStreamDefaultReader> CreateAsync(IJSRuntime jSRuntime, ReadableStream stream)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructReadableStreamDefaultReader", stream.JSReference);
        return new ReadableStreamDefaultReader(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS instance of a <see cref="ReadableStreamDefaultReader"/>.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamDefaultReader"/>.</param>
    public ReadableStreamDefaultReader(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// Reads a chunk of a stream.
    /// </summary>
    /// <returns>The next chunk of the underlying <see cref="ReadableStream"/>.</returns>
    public async Task<ReadableStreamReadResult> ReadAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("read");
        return new ReadableStreamReadResult(jSRuntime, jSInstance);
    }
}
