using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#writablestreamdefaultwriter">Streams browser specs</see>
/// </summary>
public class WritableStreamDefaultWriterInProcess : WritableStreamDefaultWriter
{
    public new IJSInProcessObjectReference JSReference;
    private readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="WritableStreamDefaultWriter"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="WritableStreamDefaultWriter"/>.</param>
    /// <returns>A wrapper instance for a <see cref="WritableStreamDefaultWriter"/>.</returns>
    public static async Task<WritableStreamDefaultWriterInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        IJSInProcessObjectReference inProcessHelper = await jSRuntime.GetInProcessHelperAsync();
        return new WritableStreamDefaultWriterInProcess(jSRuntime, inProcessHelper, jSReference);
    }

    /// <summary>
    /// Constructs a <see cref="WritableStreamDefaultWriter"/> from some <see cref="WritableStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="stream">A <see cref="WritableStream"/> wrapper instance.</param>
    /// <returns>A wrapper instance for a <see cref="WritableStreamDefaultWriter"/>.</returns>
    public static new async Task<WritableStreamDefaultWriterInProcess> CreateAsync(IJSRuntime jSRuntime, WritableStream stream)
    {
        IJSInProcessObjectReference inProcessHelper = await jSRuntime.GetInProcessHelperAsync();
        IJSInProcessObjectReference jSInstance = await inProcessHelper.InvokeAsync<IJSInProcessObjectReference>("constructWritableStreamDefaultReader", stream.JSReference);
        return new WritableStreamDefaultWriterInProcess(jSRuntime, inProcessHelper, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS instance of a <see cref="WritableStreamDefaultWriterInProcess"/>.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="inProcessHelper">An in process helper instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="WritableStreamDefaultWriterInProcess"/>.</param>
    protected internal WritableStreamDefaultWriterInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        this.inProcessHelper = inProcessHelper;
        JSReference = jSReference;
    }

    /// <summary>
    /// The size desired to fill the writers internal queue. It can be negative, if the queue is over-full.
    /// </summary>
    /// <returns>It will be null if the stream cannot be successfully written to (due to either being errored, or having an abort queued up). It will return zero if the stream is closed.</returns>
    public double? DesiredSize => inProcessHelper.Invoke<double?>("getAttribute", JSReference, "desiredSize");

    /// <summary>
    /// Releases the writer's lock.
    /// </summary>
    /// <returns></returns>
    public void ReleaseLock()
    {
        JSReference.InvokeVoid("releaseLock");
    }
}
