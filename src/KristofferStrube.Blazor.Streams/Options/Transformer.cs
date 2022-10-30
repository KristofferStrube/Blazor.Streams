using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-underlyingsink">Streams browser specs</see>
/// </summary>
public class Transformer : IDisposable
{
    protected readonly Lazy<Task<IJSObjectReference>> helperTask;
    protected readonly IJSRuntime jSRuntime;

    /// <summary>
    /// Constructs a wrapper instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns>A new <see cref="Transformer"/> wrapper instance.</returns>
    public static Transformer Create(IJSRuntime jSRuntime)
    {
        return new Transformer(jSRuntime);
    }

    /// <summary>
    /// Constructs a wrapper instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    public Transformer(IJSRuntime jSRuntime)
    {
        helperTask = new(() => jSRuntime.GetHelperAsync());
        this.jSRuntime = jSRuntime;
        ObjRef = DotNetObjectReference.Create(this);
    }

    public DotNetObjectReference<Transformer> ObjRef { get; init; }

    [JsonIgnore]
    public Func<TransformStreamDefaultController, Task>? Start { get; set; }

    [JsonIgnore]
    public Func<IJSObjectReference, TransformStreamDefaultController, Task>? Transform { get; set; }

    [JsonIgnore]
    public Func<TransformStreamDefaultController, Task>? Flush { get; set; }

    [JSInvokable]
    public async Task InvokeStart(IJSObjectReference controller)
    {
        if (Start is null)
        {
            return;
        }

        await Start.Invoke(new TransformStreamDefaultController(jSRuntime, controller));
    }

    [JSInvokable]
    public async Task InvokeTransform(IJSObjectReference chunk, IJSObjectReference controller)
    {
        if (Transform is null)
        {
            return;
        }

        await Transform.Invoke(chunk, new TransformStreamDefaultController(jSRuntime, controller));
    }

    [JSInvokable]
    public async Task InvokeFlush(IJSObjectReference controller)
    {
        if (Flush is null)
        {
            return;
        }

        await Flush.Invoke(new TransformStreamDefaultController(jSRuntime, controller));
    }

    public void Dispose()
    {
        ObjRef.Dispose();
        GC.SuppressFinalize(this);
    }
}
