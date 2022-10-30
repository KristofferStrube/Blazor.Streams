using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#readablestreamdefaultcontroller">Streams browser specs</see>
/// </summary>
public class ReadableStreamDefaultControllerInProcess : ReadableStreamDefaultController
{
    public new IJSInProcessObjectReference JSReference;
    private readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStreamDefaultController"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in process helper instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamDefaultController"/>.</param>
    internal ReadableStreamDefaultControllerInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference)
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
