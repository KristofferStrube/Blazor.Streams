using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#blqs-class">Streams browser specs</see>
/// </summary>
public class ByteLengthQueuingStrategyInProcess : ByteLengthQueuingStrategy, IJSInProcessCreatable<ByteLengthQueuingStrategyInProcess, ByteLengthQueuingStrategy>
{
    /// <summary>
    /// An in-process helper module instance from the Blazor.Streams library.
    /// </summary>
    protected readonly IJSInProcessObjectReference inProcessHelper;

    /// <inheritdoc/>
    public new IJSInProcessObjectReference JSReference { get; }

    /// <inheritdoc/>
    public static async Task<ByteLengthQueuingStrategyInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static async Task<ByteLengthQueuingStrategyInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference, CreationOptions options)
    {
        IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
        return new ByteLengthQueuingStrategyInProcess(jSRuntime, inProcesshelper, jSReference, options);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="init">An <see cref="QueuingStrategyInit"/> that provides the required <see cref="QueuingStrategyInit.HighWaterMark"/>.</param>
    /// <returns>A wrapper instance for a <see cref="ByteLengthQueuingStrategy"/>.</returns>
    public static new async Task<ByteLengthQueuingStrategy> CreateAsync(IJSRuntime jSRuntime, QueuingStrategyInit init)
    {
        IJSInProcessObjectReference inProcessHelper = await jSRuntime.GetInProcessHelperAsync();
        IJSInProcessObjectReference jSInstance = await inProcessHelper.InvokeAsync<IJSInProcessObjectReference>("constructByteLengthQueuingStrategy", init);
        return new ByteLengthQueuingStrategyInProcess(jSRuntime, inProcessHelper, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSInProcessObjectReference, CreationOptions)"/>
    protected ByteLengthQueuingStrategyInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
        this.inProcessHelper = inProcessHelper;
        JSReference = jSReference;
    }

    public double HighWaterMark => inProcessHelper.Invoke<double>("getAttribute", JSReference, "highWaterMark");

    public double Size(IJSObjectReference chunk)
    {
        return JSReference.Invoke<double>("size", chunk);
    }
}
