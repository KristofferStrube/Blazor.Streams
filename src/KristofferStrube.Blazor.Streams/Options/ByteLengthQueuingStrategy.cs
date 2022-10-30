using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

public class ByteLengthQueuingStrategy : BaseJSWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ByteLengthQueuingStrategy"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ByteLengthQueuingStrategy"/>.</param>
    /// <returns>A wrapper instance for a <see cref="ByteLengthQueuingStrategy"/>.</returns>
    public static ByteLengthQueuingStrategy Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return new ByteLengthQueuingStrategy(jSRuntime, jSReference);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="init">An <see cref="QueuingStrategyInit"/> that provides the required <see cref="QueuingStrategyInit.HighWaterMark"/>.</param>
    /// <returns>A wrapper instance for a <see cref="ByteLengthQueuingStrategy"/>.</returns>
    public static async Task<ByteLengthQueuingStrategy> CreateAsync(IJSRuntime jSRuntime, QueuingStrategyInit init)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructByteLengthQueuingStrategy", init);
        return new ByteLengthQueuingStrategy(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ByteLengthQueuingStrategy"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ByteLengthQueuingStrategy"/>.</param>
    internal ByteLengthQueuingStrategy(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task<double> GetHighWaterMarkAsync()
    {
        var helper = await helperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "highWaterMark");
    }

    public async Task<double> SizeAsync(IJSObjectReference chunk)
    {
        return await JSReference.InvokeAsync<double>("size", chunk);
    }
}
