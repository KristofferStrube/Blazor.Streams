using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-underlyingsource">Streams browser specs</see>
/// </summary>
public class UnderlyingSourceInProcess : UnderlyingSource
{
    /// <summary>
    /// An in-process helper module instance from the Blazor.Streams library.
    /// </summary>
    protected readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <returns>A new wrapper instance for a <see cref="UnderlyingSource"/>.</returns>
    public static new async Task<UnderlyingSourceInProcess> CreateAsync(IJSRuntime jSRuntime)
    {
        IJSInProcessObjectReference inProcessHelper = await jSRuntime.GetInProcessHelperAsync();
        return new UnderlyingSourceInProcess(jSRuntime, inProcessHelper);
    }

    /// <summary>
    /// Constructs a wrapper instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in process helper instance.</param>
    protected UnderlyingSourceInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper) : base(jSRuntime)
    {
        this.inProcessHelper = inProcessHelper;
        ObjRef = DotNetObjectReference.Create(this);
    }

    public new DotNetObjectReference<UnderlyingSourceInProcess> ObjRef { get; init; }

    [JsonIgnore]
    public new Action<ReadableStreamController>? Start { get; set; }

    [JsonIgnore]
    public new Action<ReadableStreamController>? Pull { get; set; }

    [JsonIgnore]
    public new Action? Cancel { get; set; }

    [JSInvokable]
    public void InvokeStart(IJSInProcessObjectReference controller)
    {
        if (Type is ReadableStreamType.Bytes)
        {
            Start?.Invoke(new ReadableByteStreamControllerInProcess(jSRuntime, inProcessHelper, controller, new() { DisposesJSReference = true }));
        }
        else
        {
            Start?.Invoke(new ReadableStreamDefaultControllerInProcess(jSRuntime, inProcessHelper, controller, new() { DisposesJSReference = true }));
        }
    }

    [JSInvokable]
    public void InvokePull(IJSInProcessObjectReference controller)
    {
        if (Type is ReadableStreamType.Bytes)
        {
            Pull?.Invoke(new ReadableByteStreamControllerInProcess(jSRuntime, inProcessHelper, controller, new() { DisposesJSReference = true }));
        }
        else
        {
            Pull?.Invoke(new ReadableStreamDefaultControllerInProcess(jSRuntime, inProcessHelper, controller, new() { DisposesJSReference = true }));
        }
    }

    [JSInvokable]
    public new void InvokeCancel()
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

    public new void Dispose()
    {
        ObjRef.Dispose();
    }
}
