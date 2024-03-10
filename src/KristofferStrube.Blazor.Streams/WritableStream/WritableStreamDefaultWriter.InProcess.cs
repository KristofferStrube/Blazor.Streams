using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#writablestreamdefaultwriter">Streams browser specs</see>
/// </summary>
public class WritableStreamDefaultWriterInProcess : WritableStreamDefaultWriter, IJSInProcessCreatable<WritableStreamDefaultWriterInProcess, WritableStreamDefaultWriter>
{
    /// <summary>
    /// An in-process helper module instance from the Blazor.Streams library.
    /// </summary>
    protected readonly IJSInProcessObjectReference inProcessHelper;

    /// <inheritdoc/>
    public new IJSInProcessObjectReference JSReference { get; }

    /// <inheritdoc/>
    public static async Task<WritableStreamDefaultWriterInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static async Task<WritableStreamDefaultWriterInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference, CreationOptions options)
    {
        IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
        return new WritableStreamDefaultWriterInProcess(jSRuntime, inProcesshelper, jSReference, options);
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
        return new WritableStreamDefaultWriterInProcess(jSRuntime, inProcessHelper, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSInProcessObjectReference, CreationOptions)"/>
    protected internal WritableStreamDefaultWriterInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
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
