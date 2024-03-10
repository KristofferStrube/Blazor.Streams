using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#ws-default-controller-class-definition">Streams browser specs</see>
/// </summary>
public class TransformStreamDefaultControllerInProcess : TransformStreamDefaultController, IJSInProcessCreatable<TransformStreamDefaultControllerInProcess, TransformStreamDefaultController>
{
    /// <summary>
    /// An in-process helper module instance from the Blazor.Streams library.
    /// </summary>
    protected readonly IJSInProcessObjectReference inProcessHelper;

    /// <inheritdoc/>
    public new IJSInProcessObjectReference JSReference { get; }

    /// <inheritdoc/>
    public static async Task<TransformStreamDefaultControllerInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static async Task<TransformStreamDefaultControllerInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference, CreationOptions options)
    {
        IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
        return new TransformStreamDefaultControllerInProcess(jSRuntime, inProcesshelper, jSReference, options);
    }

    /// <inheritdoc/>
    protected internal TransformStreamDefaultControllerInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
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
