using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#ws-class-definition">Streams browser specs</see>
/// </summary>
public partial class WritableStream : BaseJSStreamableWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="WritableStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="WritableStream"/>.</param>
    /// <returns>A wrapper instance for a <see cref="WritableStream"/>.</returns>
    [Obsolete("This will be removed in the next major release as all creator methods should be asynchronous for uniformity. Use CreateAsync instead.")]
    public static WritableStream Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return new WritableStream(jSRuntime, jSReference);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="WritableStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="WritableStream"/>.</param>
    /// <returns>A wrapper instance for a <see cref="WritableStream"/>.</returns>
    public static Task<WritableStream> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new WritableStream(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="underlyingSink">An <see cref="UnderlyingSink"/> that which implements the Start, Write, Close, and/or Abort methods.</param>
    /// <param name="strategy">A queing strategy that specifies the chunk size and a high water mark.</param>
    /// <returns>A wrapper instance for a <see cref="WritableStream"/>.</returns>
    public static async Task<WritableStream> CreateAsync(IJSRuntime jSRuntime, UnderlyingSink? underlyingSink = null, QueuingStrategy? strategy = null)
    {
        return await CreatePrivateAsync(jSRuntime, underlyingSink, strategy);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="underlyingSink">An <see cref="UnderlyingSink"/> that which implements the Start, Write, Close, and/or Abort methods.</param>
    /// <param name="strategy">A queing strategy that specifies the chunk size and a high water mark.</param>
    /// <returns>A wrapper instance for a <see cref="WritableStream"/>.</returns>
    public static async Task<WritableStream> CreateAsync(IJSRuntime jSRuntime, UnderlyingSink? underlyingSink = null, ByteLengthQueuingStrategy? strategy = null)
    {
        return await CreatePrivateAsync(jSRuntime, underlyingSink, strategy?.JSReference);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="underlyingSink">An <see cref="UnderlyingSink"/> that which implements the Start, Write, Close, and/or Abort methods.</param>
    /// <param name="strategy">A queing strategy that specifies the chunk size and a high water mark.</param>
    /// <returns>A wrapper instance for a <see cref="WritableStream"/>.</returns>
    public static async Task<WritableStream> CreateAsync(IJSRuntime jSRuntime, UnderlyingSink? underlyingSink = null, CountQueuingStrategy? strategy = null)
    {
        return await CreatePrivateAsync(jSRuntime, underlyingSink, strategy?.JSReference);
    }

    private static async Task<WritableStream> CreatePrivateAsync(IJSRuntime jSRuntime, object? underlyingSink = null, object? strategy = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructWritableStream", underlyingSink, strategy);
        return new WritableStream(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="WritableStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="WritableStream"/>.</param>
    protected WritableStream(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// Indicates whether the stream already has a writer.
    /// </summary>
    /// <returns><see langword="false"/> if the internal <c>writer</c> slot is <c>undefined</c> else return <see langword="false"/></returns>
    public async Task<bool> GetLockedAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "locked");
    }

    /// <summary>
    /// Aborts the stream if it is not locked.
    /// </summary>
    /// <returns></returns>
    public async Task AbortAsync()
    {
        await JSReference.InvokeVoidAsync("abort");
    }

    /// <summary>
    /// Closes the stream if it is not locked.
    /// </summary>
    /// <returns></returns>
    public async Task CloseAsync()
    {
        await JSReference.InvokeVoidAsync("close");
    }

    /// <summary>
    /// Creates a new <see cref="WritableStreamDefaultWriter"/> that it assigns to the internal <c>writer</c> slot and returns that.
    /// </summary>
    /// <returns>A new <see cref="WritableStreamDefaultWriter"/>.</returns>
    public async Task<WritableStreamDefaultWriter> GetWriterAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("getWriter");
        return await WritableStreamDefaultWriter.CreateAsync(jSRuntime, jSInstance);
    }
}
