using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#rs-class-definition">Streams browser specs</see>
/// </summary>
public class ReadableStreamInProcess : ReadableStream, IJSInProcessCreatable<ReadableStreamInProcess, ReadableStream>
{
    /// <summary>
    /// An in-process helper module instance from the Blazor.Streams library.
    /// </summary>
    protected readonly IJSInProcessObjectReference inProcessHelper;

    /// <inheritdoc/>
    public new IJSInProcessObjectReference JSReference { get; }

    /// <inheritdoc/>
    public static async Task<ReadableStreamInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static async Task<ReadableStreamInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference, CreationOptions options)
    {
        IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
        return new ReadableStreamInProcess(jSRuntime, inProcesshelper, jSReference, options);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="underlyingSource">An <see cref="UnderlyingSource"/> that which implements the Start, Pull, and/or Cancel methods.</param>
    /// <param name="strategy">A queing strategy that specifies the chunk size and a high water mark.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableStream"/>.</returns>
    public static new async Task<ReadableStreamInProcess> CreateAsync(IJSRuntime jSRuntime, UnderlyingSource? underlyingSource = null, QueuingStrategy? strategy = null)
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
    public static new async Task<ReadableStreamInProcess> CreateAsync(IJSRuntime jSRuntime, UnderlyingSource? underlyingSource = null, ByteLengthQueuingStrategy? strategy = null)
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
    public static new async Task<ReadableStreamInProcess> CreateAsync(IJSRuntime jSRuntime, UnderlyingSource? underlyingSource = null, CountQueuingStrategy? strategy = null)
    {
        return await CreatePrivateAsync(jSRuntime, underlyingSource, strategy?.JSReference);
    }

    private static async Task<ReadableStreamInProcess> CreatePrivateAsync(IJSRuntime jSRuntime, object? underlyingSource = null, object? strategy = null)
    {
        IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
        IJSInProcessObjectReference jSInstance = await inProcesshelper.InvokeAsync<IJSInProcessObjectReference>("constructReadableStream", underlyingSource, strategy);
        return new ReadableStreamInProcess(jSRuntime, inProcesshelper, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSInProcessObjectReference, CreationOptions)"/>
    protected internal ReadableStreamInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
        this.inProcessHelper = inProcessHelper;
        JSReference = jSReference;
    }

    /// <summary>
    /// Indicates whether the stream already has a reader.
    /// </summary>
    /// <returns><see langword="true"/> if the internal reader is a <see cref="ReadableStreamDefaultReader"/> or a <see cref="ReadableStreamBYOBReader"/> and returns <see langword="false"/> else meaning that the internal reader is <c>undefined</c></returns>
    public bool Locked => inProcessHelper.Invoke<bool>("getAttribute", JSReference, "locked");

    /// <summary>
    /// Creates a new <see cref="ReadableStreamReader"/> that it assigns to the internal <c>reader</c> slot and returns that.
    /// </summary>
    /// <param name="options">Options that can be used to indicate that a <see cref="ReadableStreamBYOBReader"/> should be created.</param>
    /// <returns>If <paramref name="options"/> is <see langword="not"/> <see langword="null"/> and the <see cref="ReadableStreamGetReaderOptions.Mode"/> is <see cref="ReadableStreamReaderMode.Byob"/> it returns a <see cref="ReadableStreamBYOBReaderInProcess"/> else it returns a <see cref="ReadableStreamDefaultReaderInProcess"/>.</returns>
    public ReadableStreamReader GetReader(ReadableStreamGetReaderOptions? options = null)
    {
        IJSInProcessObjectReference jSInstance = JSReference.Invoke<IJSInProcessObjectReference>("getReader", options);
        if (options?.Mode is ReadableStreamReaderMode.Byob)
        {
            return new ReadableStreamBYOBReaderInProcess(JSRuntime, inProcessHelper, jSInstance, new() { DisposesJSReference = true });
        }
        else
        {
            return new ReadableStreamDefaultReaderInProcess(JSRuntime, inProcessHelper, jSInstance, new() { DisposesJSReference = true });
        }
    }

    /// <summary>
    /// Helper method for creating a <see cref="ReadableStreamBYOBReaderInProcess"/> without having to instantiate the options parameter yourself and where you don't need to cast the result from <see cref="GetReader(ReadableStreamGetReaderOptions?)"/>.
    /// </summary>
    /// <returns>The <see cref="ReadableStreamBYOBReaderInProcess"/> using the <see cref="GetReader(ReadableStreamGetReaderOptions?)"/> method.</returns>
    public ReadableStreamBYOBReaderInProcess GetBYOBReader()
    {
        return (ReadableStreamBYOBReaderInProcess)GetReader(new() { Mode = ReadableStreamReaderMode.Byob });
    }

    /// <summary>
    /// Helper method for creating a <see cref="ReadableStreamDefaultReaderInProcess"/> without needing to cast the result from <see cref="GetReader(ReadableStreamGetReaderOptions?)"/>.
    /// </summary>
    /// <returns>The <see cref="ReadableStreamDefaultReaderInProcess"/> using the <see cref="GetReader(ReadableStreamGetReaderOptions?)"/> method.</returns>
    public ReadableStreamDefaultReaderInProcess GetDefaultReader()
    {
        return (ReadableStreamDefaultReaderInProcess)GetReader();
    }

    /// <summary>
    /// Provides a convenient, chainable way of piping this <see cref="ReadableStream"/> through a <see cref="TransformStream"/> or simply a <see cref="ReadableWritablePair"/>.
    /// </summary>
    /// <param name="transform">The transformer that is piped through.</param>
    /// <param name="options">An optional <see cref="StreamPipeOptions"/>.</param>
    public ReadableStream PipeThrough(IGenericTransformStreamInProcess transform, StreamPipeOptions? options = null)
    {
        IJSObjectReference jSInstance = JSReference.Invoke<IJSObjectReference>("pipeThrough", transform.JSReference, options);
        return new ReadableStream(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Tees this readable stream. Teeing a stream will lock it, preventing any other consumer from acquiring a reader.
    /// </summary>
    /// <returns>Two resulting branches as new <see cref="ReadableStream"/> instances.</returns>
    public (ReadableStream branch1, ReadableStream branch2) Tee()
    {
        IJSObjectReference jSArray = JSReference.Invoke<IJSObjectReference>("tee");
        return (
            new ReadableStream(JSRuntime, inProcessHelper.Invoke<IJSObjectReference>("elementAt", jSArray, 0), new() { DisposesJSReference = true }),
            new ReadableStream(JSRuntime, inProcessHelper.Invoke<IJSObjectReference>("elementAt", jSArray, 1), new() { DisposesJSReference = true })
            );
    }
}
