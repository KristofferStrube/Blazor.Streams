using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-underlyingsource">Streams browser specs</see>
/// </summary>
public class TransformerInProcess : Transformer
{
    private readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns>A new wrapper instance for a <see cref="TransformerInProcess"/>.</returns>
    public static async Task<TransformerInProcess> CreateAsync(IJSRuntime jSRuntime)
    {
        IJSInProcessObjectReference inProcessHelper = await jSRuntime.GetInProcessHelperAsync();
        return new TransformerInProcess(jSRuntime, inProcessHelper);
    }

    /// <summary>
    /// Constructs a wrapper instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    internal TransformerInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper) : base(jSRuntime)
    {
        this.inProcessHelper = inProcessHelper;
        ObjRef = DotNetObjectReference.Create(this);
    }

    public new DotNetObjectReference<TransformerInProcess> ObjRef { get; init; }

    [JsonIgnore]
    public new Action<TransformStreamDefaultControllerInProcess>? Start { get; set; }

    [JsonIgnore]
    public new Action<IJSObjectReference, TransformStreamDefaultControllerInProcess>? Transform { get; set; }

    [JsonIgnore]
    public new Action<TransformStreamDefaultControllerInProcess>? Flush { get; set; }

    [JSInvokable]
    public void InvokeStart(IJSInProcessObjectReference controller)
    {
        if (Start is null)
        {
            return;
        }

        Start.Invoke(new TransformStreamDefaultControllerInProcess(jSRuntime, inProcessHelper, controller));
    }

    [JSInvokable]
    public void InvokeTransform(IJSObjectReference chunk, IJSInProcessObjectReference controller)
    {
        if (Transform is null)
        {
            return;
        }

        Transform.Invoke(chunk, new TransformStreamDefaultControllerInProcess(jSRuntime, inProcessHelper, controller));
    }

    [JSInvokable]
    public void InvokeFlush(IJSInProcessObjectReference controller)
    {
        if (Flush is null)
        {
            return;
        }

        Flush.Invoke(new TransformStreamDefaultControllerInProcess(jSRuntime, inProcessHelper, controller));
    }
}
