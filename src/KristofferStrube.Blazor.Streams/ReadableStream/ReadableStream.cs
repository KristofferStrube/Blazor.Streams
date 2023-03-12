using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#rs-class-definition">Streams browser specs</see>
/// </summary>
public partial class ReadableStream : BaseJSStreamableWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStream"/>.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableStream"/>.</returns>
    public static ReadableStream Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return new ReadableStream(jSRuntime, jSReference);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="underlyingSource">An <see cref="UnderlyingSource"/> that which implements the Start, Pull, and/or Cancel methods.</param>
    /// <param name="strategy">A queing strategy that specifies the chunk size and a high water mark.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableStream"/>.</returns>
    public static async Task<ReadableStream> CreateAsync(IJSRuntime jSRuntime, UnderlyingSource? underlyingSource = null, QueuingStrategy? strategy = null)
    {
        return await CreatePrivateAsync(jSRuntime, underlyingSource, strategy);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="underlyingSource">An <see cref="UnderlyingSource"/> that which implements the Start, Pull, and/or Cancel methods.</param>
    /// <param name="strategy">A queing strategy that specifies the chunk size and a high water mark.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableStream"/>.</returns>
    public static async Task<ReadableStream> CreateAsync(IJSRuntime jSRuntime, UnderlyingSource? underlyingSource = null, ByteLengthQueuingStrategy? strategy = null)
    {
        return await CreatePrivateAsync(jSRuntime, underlyingSource, strategy?.JSReference);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="underlyingSource">An <see cref="UnderlyingSource"/> that which implements the Start, Pull, and/or Cancel methods.</param>
    /// <param name="strategy">A queing strategy that specifies the chunk size and a high water mark.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableStream"/>.</returns>
    public static async Task<ReadableStream> CreateAsync(IJSRuntime jSRuntime, UnderlyingSource? underlyingSource = null, CountQueuingStrategy? strategy = null)
    {
        return await CreatePrivateAsync(jSRuntime, underlyingSource, strategy?.JSReference);
    }

    private static async Task<ReadableStream> CreatePrivateAsync(IJSRuntime jSRuntime, object? underlyingSource = null, object? strategy = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructReadableStream", underlyingSource, strategy);
        return new ReadableStream(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStream"/>.</param>
    internal ReadableStream(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// Indicates whether the stream already has a reader.
    /// </summary>
    /// <returns><see langword="true"/> if the internal reader is a <see cref="ReadableStreamDefaultReader"/> or a <see cref="ReadableStreamBYOBReader"/> and returns <see langword="false"/> else meaning that the internal reader is <c>undefined</c></returns>
    public async Task<bool> GetLockedAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "locked");
    }

    /// <summary>
    /// Closes the internal reader if it is not locked.
    /// </summary>
    /// <returns></returns>
    public async Task CancelAsync()
    {
        await JSReference.InvokeVoidAsync("cancel");
    }

    /// <summary>
    /// Creates a new <see cref="ReadableStreamReader"/> that it assigns to the internal <c>reader</c> slot and returns that.
    /// </summary>
    /// <param name="options">Options that can be used to indicate that a <see cref="ReadableStreamBYOBReader"/> should be created.</param>
    /// <returns>If <paramref name="options"/> is <see langword="not"/> <see langword="null"/> and the <see cref="ReadableStreamGetReaderOptions.Mode"/> is <see cref="ReadableStreamReaderMode.Byob"/> it returns a <see cref="ReadableStreamBYOBReader"/> else it returns a <see cref="ReadableStreamDefaultReader"/>.</returns>
    public async Task<ReadableStreamReader> GetReaderAsync(ReadableStreamGetReaderOptions? options = null)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("getReader", options);
        if (options?.Mode is ReadableStreamReaderMode.Byob)
        {
            return new ReadableStreamBYOBReader(jSRuntime, jSInstance);
        }
        else
        {
            return new ReadableStreamDefaultReader(jSRuntime, jSInstance);
        }
    }

    /// <summary>
    /// Helper method for creating a <see cref="ReadableStreamBYOBReader"/> without having to instantiate the options parameter yourself and where you don't need to cast the result from <see cref="GetReaderAsync(ReadableStreamGetReaderOptions?)"/>.
    /// </summary>
    /// <returns>The <see cref="ReadableStreamBYOBReader"/> using the <see cref="GetReaderAsync(ReadableStreamGetReaderOptions?)"/> method.</returns>
    public async Task<ReadableStreamBYOBReader> GetBYOBReaderAsync()
    {
        return (ReadableStreamBYOBReader)await GetReaderAsync(new() { Mode = ReadableStreamReaderMode.Byob });
    }

    /// <summary>
    /// Helper method for creating a <see cref="ReadableStreamDefaultReader"/> without needing to cast the result from <see cref="GetReaderAsync(ReadableStreamGetReaderOptions?)"/>.
    /// </summary>
    /// <returns>The <see cref="ReadableStreamDefaultReader"/> using the <see cref="GetReaderAsync(ReadableStreamGetReaderOptions?)"/> method.</returns>
    public async Task<ReadableStreamDefaultReader> GetDefaultReaderAsync()
    {
        return (ReadableStreamDefaultReader)await GetReaderAsync();
    }

    /// <summary>
    /// Provides a convenient, chainable way of piping this <see cref="ReadableStream"/> through a <see cref="TransformStream"/> or simply a <see cref="ReadableWritablePair"/>.
    /// </summary>
    /// <param name="transform">The transformer that is piped through.</param>
    /// <param name="options">An optional <see cref="StreamPipeOptions"/>.</param>
    /// <returns></returns>
    public async Task<ReadableStream> PipeThroughAsync(IGenericTransformStream transform, StreamPipeOptions? options = null)
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("pipeThrough", transform.JSReference, options);
        return new ReadableStream(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Pipes this readable stream to a given writable stream destination.
    /// </summary>
    /// <param name="destination">The <see cref="WritableStream"/> that is piped to.</param>
    /// <param name="options">An optional <see cref="StreamPipeOptions"/>.</param>
    /// <returns></returns>
    public async Task PipeToAsync(WritableStream destination, StreamPipeOptions? options = null)
    {
        await JSReference.InvokeVoidAsync("pipeTo", destination.JSReference, options);
    }

    /// <summary>
    /// Tees this readable stream. Teeing a stream will lock it, preventing any other consumer from acquiring a reader.
    /// </summary>
    /// <returns>Two resulting branches as new <see cref="ReadableStream"/> instances.</returns>
    public async Task<(ReadableStream branch1, ReadableStream branch2)> TeeAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSArray = await JSReference.InvokeAsync<IJSObjectReference>("tee");
        return (
            new ReadableStream(jSRuntime, await helper.InvokeAsync<IJSObjectReference>("elementAt", jSArray, 0)),
            new ReadableStream(jSRuntime, await helper.InvokeAsync<IJSObjectReference>("elementAt", jSArray, 1))
            );
    }
}
