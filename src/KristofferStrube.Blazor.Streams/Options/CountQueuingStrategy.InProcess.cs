using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

public class CountQueuingStrategyInProcess : CountQueuingStrategy
{
    public new IJSInProcessObjectReference JSReference;
    private readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="CountQueuingStrategy"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="CountQueuingStrategy"/>.</param>
    /// <returns>A wrapper instance for a <see cref="CountQueuingStrategy"/>.</returns>
    public static async Task<CountQueuingStrategyInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        IJSInProcessObjectReference inProcessHelper = await jSRuntime.GetInProcessHelperAsync();
        return new CountQueuingStrategyInProcess(jSRuntime, inProcessHelper, jSReference);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="init">An <see cref="QueuingStrategyInit"/> that provides the required <see cref="QueuingStrategyInit.HighWaterMark"/>.</param>
    /// <returns>A wrapper instance for a <see cref="CountQueuingStrategy"/>.</returns>
    public static new async Task<CountQueuingStrategyInProcess> CreateAsync(IJSRuntime jSRuntime, QueuingStrategyInit init)
    {
        IJSInProcessObjectReference inProcessHelper = await jSRuntime.GetInProcessHelperAsync();
        IJSInProcessObjectReference jSInstance = await inProcessHelper.InvokeAsync<IJSInProcessObjectReference>("constructCountQueuingStrategy", init);
        return new CountQueuingStrategyInProcess(jSRuntime, inProcessHelper, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="CountQueuingStrategy"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in process helper instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="CountQueuingStrategy"/>.</param>
    protected CountQueuingStrategyInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference)
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
