using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#ws-default-controller-class-definition">Streams browser specs</see>
/// </summary>
public class TransformStreamDefaultControllerInProcess : TransformStreamDefaultController
{
    public new IJSInProcessObjectReference JSReference;
    private readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="TransformStreamDefaultController"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="TransformStreamDefaultController"/>.</param>
    /// <returns>A wrapper instance for a <see cref="TransformStreamDefaultController"/>.</returns>
    public static async Task<TransformStreamDefaultControllerInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        IJSInProcessObjectReference inProcessHelper = await jSRuntime.GetInProcessHelperAsync();
        return new TransformStreamDefaultControllerInProcess(jSRuntime, inProcessHelper, jSReference);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="TransformStreamDefaultController"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in process helper instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="TransformStreamDefaultController"/>.</param>
    internal TransformStreamDefaultControllerInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference)
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
    /// Enqueues a chunk on the readable side of the transfor stream.
    /// </summary>
    /// <param name="chunk">A JS reference to a chunk.</param>
    /// <returns></returns>
    public void Enqueue(IJSObjectReference chunk)
    {
        JSReference.InvokeVoid("enqueue", chunk);
    }

    /// <summary>
    /// Errors both the readable and writable side of the transform stream.
    /// </summary>
    /// <returns></returns>
    public void Error()
    {
        JSReference.InvokeVoid("error");
    }

    /// <summary>
    /// Closes the readable side and errors the writable side.
    /// </summary>
    /// <returns></returns>
    public void Terminate()
    {
        JSReference.InvokeVoid("terminate");
    }
}
