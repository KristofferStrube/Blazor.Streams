using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-underlyingsource">Streams browser specs</see>
/// </summary>
public class UnderlyingSinkInProcess : UnderlyingSink
{
    private readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns>A new wrapper instance for a <see cref="UnderlyingSinkInProcess"/>.</returns>
    public static async Task<UnderlyingSinkInProcess> CreateAsync(IJSRuntime jSRuntime)
    {
        IJSInProcessObjectReference inProcessHelper = await jSRuntime.GetInProcessHelperAsync();
        return new UnderlyingSinkInProcess(jSRuntime, inProcessHelper);
    }

    /// <summary>
    /// Constructs a wrapper instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    internal UnderlyingSinkInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper) : base(jSRuntime)
    {
        this.inProcessHelper = inProcessHelper;
        ObjRef = DotNetObjectReference.Create(this);
    }

    public new DotNetObjectReference<UnderlyingSinkInProcess> ObjRef { get; init; }

    [JsonIgnore]
    public new Action<WritableStreamDefaultControllerInProcess>? Start { get; set; }

    [JsonIgnore]
    public new Action<IJSObjectReference, WritableStreamDefaultControllerInProcess>? Write { get; set; }

    [JsonIgnore]
    public new Action? Close { get; set; }

    [JsonIgnore]
    public new Action? Abort { get; set; }

    [JSInvokable]
    public void InvokeStart(IJSInProcessObjectReference controller)
    {
        if (Start is null)
        {
            return;
        }

        Start.Invoke(new WritableStreamDefaultControllerInProcess(jSRuntime, inProcessHelper, controller));
    }

    [JSInvokable]
    public void InvokeWrite(IJSObjectReference chunk, IJSInProcessObjectReference controller)
    {
        if (Write is null)
        {
            return;
        }

        Write.Invoke(chunk, new WritableStreamDefaultControllerInProcess(jSRuntime, inProcessHelper, controller));
    }

    [JSInvokable]
    public new void InvokeClose()
    {
        if (Close is null)
        {
            return;
        }

        Close.Invoke();
    }

    [JSInvokable]
    public new void InvokeAbort()
    {
        if (Abort is null)
        {
            return;
        }

        Abort.Invoke();
    }

    public new void Dispose()
    {
        ObjRef.Dispose();
        GC.SuppressFinalize(this);
    }
}
