using Microsoft.JSInterop;
using System.Text.Json.Serialization;
using System.Threading.Channels;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-underlyingsink">Streams browser specs</see>
/// </summary>
public class UnderlyingSink : IDisposable
{
    protected readonly Lazy<Task<IJSObjectReference>> helperTask;
    protected readonly IJSRuntime jSRuntime;

    /// <summary>
    /// Constructs a wrapper instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns>A new <see cref="UnderlyingSink"/> wrapper instance.</returns>
    public static UnderlyingSink Create(IJSRuntime jSRuntime)
    {
        return new UnderlyingSink(jSRuntime);
    }

    /// <summary>
    /// Constructs a wrapper instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    public UnderlyingSink(IJSRuntime jSRuntime)
    {
        helperTask = new(() => jSRuntime.GetHelperAsync());
        this.jSRuntime = jSRuntime;
        ObjRef = DotNetObjectReference.Create(this);
    }

    public DotNetObjectReference<UnderlyingSink> ObjRef { get; init; }

    [JsonIgnore]
    public Func<WritableStreamDefaultController, Task>? Start { get; set; }

    [JsonIgnore]
    public Func<IJSObjectReference, WritableStreamDefaultController, Task>? Write { get; set; }

    [JsonIgnore]
    public Func<Task>? Close { get; set; }

    [JsonIgnore]
    public Func<Task>? Abort { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("type")]
    public ReadableStreamType? Type { get; set; }

    [JSInvokable]
    public async Task InvokeStart(IJSObjectReference controller)
    {
        if (Start is null) return;
        await Start.Invoke(new WritableStreamDefaultController(jSRuntime, controller));
    }

    [JSInvokable]
    public async Task InvokeWrite(IJSObjectReference chunk, IJSObjectReference controller)
    {
        if (Write is null) return;
        await Write.Invoke(chunk, new WritableStreamDefaultController(jSRuntime, controller));
    }

    [JSInvokable]
    public async Task InvokeClose()
    {
        if (Close is null) return;
        await Close.Invoke();
    }

    [JSInvokable]
    public async Task InvokeAbort()
    {
        if (Abort is null) return;
        await Abort.Invoke();
    }

    public void Dispose()
    {
        ObjRef.Dispose();
        GC.SuppressFinalize(this);
    }
}
