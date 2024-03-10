using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#readablestreamdefaultcontroller">Streams browser specs</see>
/// </summary>
public class ReadableStreamDefaultControllerInProcess : ReadableStreamDefaultController, IJSInProcessCreatable<ReadableStreamDefaultControllerInProcess, ReadableStreamDefaultController>
{
    /// <summary>
    /// An in-process helper module instance from the Blazor.Streams library.
    /// </summary>
    protected readonly IJSInProcessObjectReference inProcessHelper;

    /// <inheritdoc/>
    public new IJSInProcessObjectReference JSReference { get; }

    /// <inheritdoc/>
    public static async Task<ReadableStreamDefaultControllerInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static async Task<ReadableStreamDefaultControllerInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference, CreationOptions options)
    {
        IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
        return new ReadableStreamDefaultControllerInProcess(jSRuntime, inProcesshelper, jSReference, options);
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSInProcessObjectReference, CreationOptions)"/>
    internal ReadableStreamDefaultControllerInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
        this.inProcessHelper = inProcessHelper;
        JSReference = jSReference;
    }

    /// <summary>
    /// The desired size to fill the controlled stream's internal queue.
    /// </summary>
    /// <returns>Negative values means that the queue is overfull.</returns>
    public double? DesiredSize => inProcessHelper.Invoke<double?>("getAttribute", JSReference, "desiredSize");

    /// <summary>
    /// Closes the controlled stream once all previously enqueued chunks have been read.
    /// </summary>
    /// <returns></returns>
    public void Close()
    {
        JSReference.InvokeVoid("close");
    }

    /// <summary>
    /// Enqueues the chunk in the controlled stream.
    /// </summary>
    /// <param name="chunk">A JS reference to a chunk.</param>
    /// <returns></returns>
    public void Enqueue(IJSObjectReference chunk)
    {
        JSReference.InvokeVoid("enqueue", chunk);
    }

    /// <summary>
    /// Errors the controlled stream so that all future interactions will fail.
    /// </summary>
    /// <returns></returns>
    public void Error()
    {
        JSReference.InvokeVoid("error");
    }
}
