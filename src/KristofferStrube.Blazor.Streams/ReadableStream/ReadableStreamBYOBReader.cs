using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#readablestreambyobreader">Streams browser specs</see>
/// </summary>
public class ReadableStreamBYOBReader : ReadableStreamReader, IJSCreatable<ReadableStreamBYOBReader>
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStreamBYOBReader"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamBYOBReader"/>.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableStreamBYOBReader"/>.</returns>
    [Obsolete("This will be removed in the next major release as all creator methods should be asynchronous for uniformity. Use CreateAsync instead.")]
    public static ReadableStreamBYOBReader Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return new ReadableStreamBYOBReader(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static async Task<ReadableStreamBYOBReader> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static Task<ReadableStreamBYOBReader> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new ReadableStreamBYOBReader(jSRuntime, jSReference, options));
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
        return new ReadableStreamBYOBReader(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected internal ReadableStreamBYOBReader(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }

    /// <summary>
    /// Reads a chunk of a stream.
    /// </summary>
    /// <param name="view">The <see cref="IArrayBufferView"/> that acts as a buffer.</param>
    /// <returns>The next chunk of the underlying <see cref="ReadableStream"/>.</returns>
    public async Task<ReadableStreamReadResult> ReadAsync(IArrayBufferView view)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("read", view.JSReference);
        return new ReadableStreamReadResult(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Reads a chunk of a stream.
    /// </summary>
    /// <param name="view">The <see cref="IArrayBufferView"/> that acts as a buffer.</param>
    /// <param name="options">The options for how the chunk is to be read.</param>
    /// <returns>The next chunk of the underlying <see cref="ReadableStream"/>.</returns>
    public async Task<ReadableStreamReadResult> ReadAsync(IArrayBufferView view, ReadableStreamBYOBReaderReadOptions? options = null)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("read", view.JSReference, options);
        return new ReadableStreamReadResult(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }
}
