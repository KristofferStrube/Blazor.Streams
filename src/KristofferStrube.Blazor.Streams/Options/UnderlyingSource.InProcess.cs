using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-underlyingsource">Streams browser specs</see>
/// </summary>
public class UnderlyingSourceInProcess : IDisposable
{
    protected readonly IJSRuntime jSRuntime;
    private readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns>A new <see cref="UnderlyingSourceInProcess"/> wrapper instance.</returns>
    public static async Task<UnderlyingSourceInProcess> CreateAsync(IJSRuntime jSRuntime)
    {
        var inProcessHelper = await jSRuntime.GetInProcessHelperAsync();
        return new UnderlyingSourceInProcess(jSRuntime, inProcessHelper);
    }

    /// <summary>
    /// Constructs a wrapper instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    internal UnderlyingSourceInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper)
    {
        this.jSRuntime = jSRuntime;
        this.inProcessHelper = inProcessHelper;
        ObjRef = DotNetObjectReference.Create(this);
    }

    public DotNetObjectReference<UnderlyingSourceInProcess> ObjRef { get; init; }

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
    public void InvokeStart(IJSInProcessObjectReference controller)
    {
        if (Type is ReadableStreamType.Bytes)
        {
            Start?.Invoke(new ReadableByteStreamControllerInProcess(jSRuntime, inProcessHelper, controller));
        }
        else
        {
            Start?.Invoke(new ReadableStreamDefaultControllerInProcess(jSRuntime, inProcessHelper, controller));
        }
    }

    [JSInvokable]
    public void InvokePull(IJSInProcessObjectReference controller)
    {
        if (Type is ReadableStreamType.Bytes)
        {
            Pull?.Invoke(new ReadableByteStreamControllerInProcess(jSRuntime, inProcessHelper, controller));
        }
        else
        {
            Pull?.Invoke(new ReadableStreamDefaultControllerInProcess(jSRuntime, inProcessHelper, controller));
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
