using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#rbs-controller-class">Streams browser specs</see>
/// </summary>
public class ReadableByteStreamControllerInProcess : ReadableByteStreamController
{
    public new IJSInProcessObjectReference JSReference;
    private readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableByteStreamController"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in process helper instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableByteStreamController"/>.</param>
    internal ReadableByteStreamControllerInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        this.inProcessHelper = inProcessHelper;
        JSReference = jSReference;
    }

    /// <summary>
    /// Returns the current BYOB pull request, or null if there isn’t one.
    /// </summary>
    /// <returns>A <see cref="ReadableStreamBYOBRequestInProcess"/></returns>
    public ReadableStreamBYOBRequestInProcess? BYOBRequest
    {
        get
        {
            var jSInstance = inProcessHelper.Invoke<IJSInProcessObjectReference?>("getAttribute", JSReference, "byobRequest");
            if (jSInstance is null)
            {
                return null;
            }
            return new ReadableStreamBYOBRequestInProcess(jSRuntime, inProcessHelper, jSInstance);
        }
    }

    /// <summary>
    /// The desired size to fill the controlled stream's internal queue.
    /// </summary>
    /// <returns>Negative values mean that the queue is overfull.</returns>
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
    /// <param name="chunk"></param>
    /// <returns></returns>
    public void Enqueue(ArrayBufferView chunk)
    {
        JSReference.InvokeVoid("enqueue", chunk.JSReference);
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
