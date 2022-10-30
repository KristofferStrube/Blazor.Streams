using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-underlyingsource">Streams browser specs</see>
/// </summary>
public class UnderlyingSinkInProcess : IDisposable
{
    protected readonly IJSRuntime jSRuntime;
    private readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns>A new <see cref="UnderlyingSinkInProcess"/> wrapper instance.</returns>
    public static async Task<UnderlyingSinkInProcess> CreateAsync(IJSRuntime jSRuntime)
    {
        var inProcessHelper = await jSRuntime.GetInProcessHelperAsync();
        return new UnderlyingSinkInProcess(jSRuntime, inProcessHelper);
    }

    /// <summary>
    /// Constructs a wrapper instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    internal UnderlyingSinkInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper)
    {
        this.jSRuntime = jSRuntime;
        this.inProcessHelper = inProcessHelper;
        ObjRef = DotNetObjectReference.Create(this);
    }

    public DotNetObjectReference<UnderlyingSinkInProcess> ObjRef { get; init; }

    [JsonIgnore]
    public Action<WritableStreamDefaultController>? Start { get; set; }

    [JsonIgnore]
    public Action<IJSObjectReference, WritableStreamDefaultController>? Write { get; set; }

    [JsonIgnore]
    public Action? Close { get; set; }

    [JsonIgnore]
    public Action? Abort { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("type")]
    public ReadableStreamType? Type { get; set; }

    [JsonPropertyName("autoAllocateChunkSize")]
    public ulong AutoAllocateChunkSize { get; set; }

    [JSInvokable]
    public void InvokeStart(IJSObjectReference controller)
    {
        if (Start is null) return;
        Start.Invoke(new WritableStreamDefaultController(jSRuntime, controller));
    }

    [JSInvokable]
    public void InvokeWrite(IJSObjectReference chunk, IJSObjectReference controller)
    {
        if (Write is null) return;
        Write.Invoke(chunk, new WritableStreamDefaultController(jSRuntime, controller));
    }

    [JSInvokable]
    public void InvokeClose()
    {
        if (Close is null) return;
        Close.Invoke();
    }

    [JSInvokable]
    public void InvokeAbort()
    {
        if (Abort is null) return;
        Abort.Invoke();
    }

    public void Dispose()
    {
        ObjRef.Dispose();
        GC.SuppressFinalize(this);
    }
}
