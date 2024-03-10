using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-underlyingsource">Streams browser specs</see>
/// </summary>
public class TransformerInProcess : Transformer
{
    /// <summary>
    /// An in-process helper module instance from the Blazor.Streams library.
    /// </summary>
    protected readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns>A new wrapper instance for a <see cref="TransformerInProcess"/>.</returns>
    public new static async Task<TransformerInProcess> CreateAsync(IJSRuntime jSRuntime)
    {
        IJSInProcessObjectReference inProcessHelper = await jSRuntime.GetInProcessHelperAsync();
        return new TransformerInProcess(jSRuntime, inProcessHelper);
    }

    /// <summary>
    /// Constructs a wrapper instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in process helper instance.</param>
    protected TransformerInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper) : base(jSRuntime)
    {
        this.inProcessHelper = inProcessHelper;
        ObjRef = DotNetObjectReference.Create(this);
    }

    public new DotNetObjectReference<TransformerInProcess> ObjRef { get; init; }

    /// <summary>
    /// A function that is called immediately during creation of the <see cref="TransformStream"/>.
    /// </summary>
    [JsonIgnore]
    public new Action<TransformStreamDefaultControllerInProcess>? Start { get; set; }

    /// <summary>
    /// A function called when a new chunk originally written to the writable side is ready to be transformed.
    /// </summary>
    [JsonIgnore]
    public new Action<IJSObjectReference, TransformStreamDefaultControllerInProcess>? Transform { get; set; }

    /// <summary>
    /// A function called after all chunks written to the writable side have been transformed by successfully passing through <see cref="Transform"/>, and the writable side is about to be closed.
    /// </summary>
    [JsonIgnore]
    public new Action<TransformStreamDefaultControllerInProcess>? Flush { get; set; }

    /// <summary>
    /// A function called when the readable side is cancelled, or when the writable side is aborted.
    /// </summary>
    [JsonIgnore]
    public new Action<IJSObjectReference>? Cancel { get; set; }

    [JSInvokable]
    public void InvokeStart(IJSInProcessObjectReference controller)
    {
        if (Start is null)
        {
            return;
        }

        Start.Invoke(new TransformStreamDefaultControllerInProcess(jSRuntime, inProcessHelper, controller, new() { DisposesJSReference = true }));
    }

    [JSInvokable]
    public void InvokeTransform(IJSObjectReference chunk, IJSInProcessObjectReference controller)
    {
        if (Transform is null)
        {
            return;
        }

        Transform.Invoke(chunk, new TransformStreamDefaultControllerInProcess(jSRuntime, inProcessHelper, controller, new() { DisposesJSReference = true }));
    }

    [JSInvokable]
    public void InvokeFlush(IJSInProcessObjectReference controller)
    {
        if (Flush is null)
        {
            return;
        }

        Flush.Invoke(new TransformStreamDefaultControllerInProcess(jSRuntime, inProcessHelper, controller, new() { DisposesJSReference = true }));
    }

    [JSInvokable]
    public void InvokeCancel(IJSInProcessObjectReference reason)
    {
        if (Cancel is null)
        {
            return;
        }

        Cancel.Invoke(reason);
    }
}
