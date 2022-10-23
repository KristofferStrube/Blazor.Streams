using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-underlyingsource">Streams browser specs</see>
/// </summary>
public class UnderlyingSource : IDisposable
{
    protected readonly Lazy<Task<IJSObjectReference>> helperTask;
    protected readonly IJSRuntime jSRuntime;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="UnderlyingSource"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    public UnderlyingSource(IJSRuntime jSRuntime)
    {
        helperTask = new(() => jSRuntime.GetHelperAsync());
        this.jSRuntime = jSRuntime;
        ObjRef = DotNetObjectReference.Create(this);
    }

    public DotNetObjectReference<UnderlyingSource> ObjRef { get; init; }

    [JsonIgnore]
    public Action<ReadableStreamController>? Start { get; set; }

    [JsonIgnore]
    public Action<ReadableStreamController>? Pull { get; set; }

    [JsonIgnore]
    public Action? Cancel { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("type")]
    public ReadableStreamType? Type { get; set; }

    [JsonPropertyName("autoAllocateChunkSize")]
    public ulong AutoAllocateChunkSize { get; set; }

    [JSInvokable]
    public void InvokeStart(IJSObjectReference controller)
    {
        if (Type is ReadableStreamType.Bytes)
        {
            Start?.Invoke(new ReadableByteStreamController(jSRuntime, controller));
        }
        else
        {
            Start?.Invoke(new ReadableStreamDefaultController(jSRuntime, controller));
        }
    }

    [JSInvokable]
    public void InvokePull(IJSObjectReference controller)
    {
        if (Type is ReadableStreamType.Bytes)
        {
            Pull?.Invoke(new ReadableByteStreamController(jSRuntime, controller));
        }
        else
        {
            Pull?.Invoke(new ReadableStreamDefaultController(jSRuntime, controller));
        }
    }

    [JSInvokable]
    public void InvokeCancel()
    {
        if (Type is ReadableStreamType.Bytes)
        {
            Cancel?.Invoke();
        }
        else
        {
            Cancel?.Invoke();
        }
    }

    public void Dispose()
    {
        ObjRef.Dispose();
        GC.SuppressFinalize(this);
    }
}
