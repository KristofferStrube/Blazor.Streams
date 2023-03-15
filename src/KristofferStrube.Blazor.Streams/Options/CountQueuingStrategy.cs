using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

public class CountQueuingStrategy : BaseJSWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="CountQueuingStrategy"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="CountQueuingStrategy"/>.</param>
    /// <returns>A wrapper instance for a <see cref="CountQueuingStrategy"/>.</returns>
    [Obsolete("This will be removed in the next major release as all creator methods should be asynchronous for uniformity. Use CreateAsync instead.")]
    public static CountQueuingStrategy Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return new CountQueuingStrategy(jSRuntime, jSReference);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="CountQueuingStrategy"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="CountQueuingStrategy"/>.</param>
    /// <returns>A wrapper instance for a <see cref="CountQueuingStrategy"/>.</returns>
    public static Task<CountQueuingStrategy> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new CountQueuingStrategy(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="init">An <see cref="QueuingStrategyInit"/> that provides the required <see cref="QueuingStrategyInit.HighWaterMark"/>.</param>
    /// <returns>A wrapper instance for a <see cref="CountQueuingStrategy"/>.</returns>
    public static async Task<CountQueuingStrategy> CreateAsync(IJSRuntime jSRuntime, QueuingStrategyInit init)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructCountQueuingStrategy", init);
        return new CountQueuingStrategy(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="CountQueuingStrategy"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="CountQueuingStrategy"/>.</param>
    protected CountQueuingStrategy(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task<double> GetHighWaterMarkAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<double>("getAttribute", JSReference, "highWaterMark");
    }

    public async Task<double> SizeAsync(IJSObjectReference chunk)
    {
        return await JSReference.InvokeAsync<double>("size", chunk);
    }
}
