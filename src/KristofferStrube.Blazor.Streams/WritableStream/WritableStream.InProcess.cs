using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#ws-class-definition">Streams browser specs</see>
/// </summary>
public class WritableStreamInProcess : WritableStream
{
    public new IJSInProcessObjectReference JSReference;
    private readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="WritableStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="WritableStream"/>.</param>
    /// <returns>A wrapper instance for a <see cref="WritableStream"/>.</returns>
    public static async Task<WritableStreamInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        IJSInProcessObjectReference inProcessHelper = await jSRuntime.GetInProcessHelperAsync();
        return new WritableStreamInProcess(jSRuntime, inProcessHelper, jSReference);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="underlyingSink">An <see cref="UnderlyingSink"/> that which implements the Start, Write, Close, and/or Abort methods.</param>
    /// <param name="strategy">A queing strategy that specifies the chunk size and a high water mark.</param>
    /// <returns>A wrapper instance for a <see cref="WritableStream"/>.</returns>
    public static new async Task<WritableStreamInProcess> CreateAsync(IJSRuntime jSRuntime, UnderlyingSink? underlyingSink = null, QueuingStrategy? strategy = null)
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
    public static new async Task<WritableStreamInProcess> CreateAsync(IJSRuntime jSRuntime, UnderlyingSink? underlyingSink = null, ByteLengthQueuingStrategy? strategy = null)
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
    public static new async Task<WritableStreamInProcess> CreateAsync(IJSRuntime jSRuntime, UnderlyingSink? underlyingSink = null, CountQueuingStrategy? strategy = null)
    {
        return await CreatePrivateAsync(jSRuntime, underlyingSink, strategy?.JSReference);
    }

    private static async Task<WritableStreamInProcess> CreatePrivateAsync(IJSRuntime jSRuntime, object? underlyingSink = null, object? strategy = null)
    {
        IJSInProcessObjectReference inProcessHelper = await jSRuntime.GetInProcessHelperAsync();
        IJSInProcessObjectReference jSInstance = await inProcessHelper.InvokeAsync<IJSInProcessObjectReference>("constructWritableStream", underlyingSink, strategy);
        return new WritableStreamInProcess(jSRuntime, inProcessHelper, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="WritableStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in process helper instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="WritableStream"/>.</param>
    protected internal WritableStreamInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        this.inProcessHelper = inProcessHelper;
        JSReference = jSReference;
    }

    /// <summary>
    /// Indicates whether the stream already has a writer.
    /// </summary>
    /// <returns><see langword="false"/> if the internal <c>writer</c> slot is <c>undefined</c> else return <see langword="false"/></returns>
    public bool Locked => inProcessHelper.Invoke<bool>("getAttribute", JSReference, "locked");


    /// <summary>
    /// Creates a new <see cref="WritableStreamDefaultWriter"/> that it assigns to the internal <c>writer</c> slot and returns that.
    /// </summary>
    /// <returns>A new <see cref="WritableStreamDefaultWriter"/>.</returns>
    public WritableStreamDefaultWriterInProcess GetWriter()
    {
        IJSInProcessObjectReference jSInstance = JSReference.Invoke<IJSInProcessObjectReference>("getWriter");
        return new WritableStreamDefaultWriterInProcess(jSRuntime, inProcessHelper, jSInstance);
    }
}
