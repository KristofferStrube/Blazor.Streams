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
    /// Constructs a wrapper instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns>A new <see cref="UnderlyingSource"/> wrapper instance.</returns>
    [Obsolete("This will be removed in the next major release as all creator methods should be asynchronous for uniformity. Use CreateAsync instead.")]
    public static UnderlyingSource Create(IJSRuntime jSRuntime)
    {
        return new UnderlyingSource(jSRuntime);
    }

    /// <summary>
    /// Constructs a wrapper instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns>A new <see cref="UnderlyingSource"/> wrapper instance.</returns>
    public static Task<UnderlyingSource> CreateAsync(IJSRuntime jSRuntime)
    {
        return Task.FromResult(new UnderlyingSource(jSRuntime));
    }

    /// <summary>
    /// Constructs a wrapper instance.
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
    public Func<ReadableStreamController, Task>? Start { get; set; }

    [JsonIgnore]
    public Func<ReadableStreamController, Task>? Pull { get; set; }

    [JsonIgnore]
    public Func<Task>? Cancel { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("type")]
    public ReadableStreamType? Type { get; set; }

    [JsonPropertyName("autoAllocateChunkSize")]
    public ulong AutoAllocateChunkSize { get; set; }

    [JSInvokable]
    public async Task InvokeStart(IJSObjectReference controller)
    {
        if (Start is null)
        {
            return;
        }

        if (Type is ReadableStreamType.Bytes)
        {
            await Start.Invoke(new ReadableByteStreamController(jSRuntime, controller));
        }
        else
        {
            await Start.Invoke(new ReadableStreamDefaultController(jSRuntime, controller));
        }
    }

    [JSInvokable]
    public async Task InvokePull(IJSObjectReference controller)
    {
        if (Pull is null)
        {
            return;
        }

        if (Type is ReadableStreamType.Bytes)
        {
            await Pull.Invoke(new ReadableByteStreamController(jSRuntime, controller));
        }
        else
        {
            await Pull.Invoke(new ReadableStreamDefaultController(jSRuntime, controller));
        }
    }

    [JSInvokable]
    public async Task InvokeCancel()
    {
        if (Cancel is null)
        {
            return;
        }

        if (Type is ReadableStreamType.Bytes)
        {
            await Cancel.Invoke();
        }
        else
        {
            await Cancel.Invoke();
        }
    }

    public void Dispose()
    {
        ObjRef.Dispose();
        GC.SuppressFinalize(this);
    }
}
