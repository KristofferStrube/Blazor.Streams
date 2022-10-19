using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#rs">Streams browser specs</see>
/// </summary>
public class ReadableStream : IAsyncDisposable
{
    public readonly IJSObjectReference JSReference;
    protected readonly Lazy<Task<IJSObjectReference>> helperTask;
    protected readonly IJSRuntime jSRuntime;

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="underlyingSource">A JS reference to an object equivalent to a <see href="https://streams.spec.whatwg.org/#dictdef-underlyingsource">JS UnderlyingSource</see>.</param>
    /// <param name="strategy">A queing strategy that specifies the chunk size and a high water mark.</param>
    /// <returns>A wrapper instance for a ReadableStream.</returns>
    public static async Task<ReadableStream> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference? underlyingSource = null, QueingStrategy? strategy = null)
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
    public ReadableStream(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        helperTask = new(() => jSRuntime.GetHelperAsync());
        JSReference = jSReference;
        this.jSRuntime = jSRuntime;
    }

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
    /// Closes the internal reader if it is locked.
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

    public async ValueTask DisposeAsync()
    {
        if (helperTask.IsValueCreated)
        {
            IJSObjectReference module = await helperTask.Value;
            await module.DisposeAsync();
        }
        GC.SuppressFinalize(this);
    }
}
