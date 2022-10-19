using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-readablestreamreadresult">Streams browser specs</see>
/// </summary>
public class ReadableStreamReadResult
{
    public readonly IJSObjectReference JSReference;
    protected readonly Lazy<Task<IJSObjectReference>> helperTask;
    private readonly IJSRuntime jSRuntime;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStreamReadResult"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamReadResult"/>.</param>
    internal ReadableStreamReadResult(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        helperTask = new(() => jSRuntime.GetHelperAsync());
        JSReference = jSReference;
        this.jSRuntime = jSRuntime;
    }

    /// <summary>
    /// A JS Reference to a chunk of data.
    /// </summary>
    /// <returns>A <see cref="IJSObjectReference"/> to a value which can be of <c>any</c> type.</returns>
    public async Task<IJSObjectReference> GetValueAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "value");
    }

    /// <summary>
    /// Indicates whether this is the last read which means that <see cref="GetValueAsync"/> will be <c>undefined</c>.
    /// </summary>
    /// <returns><see langword="true"/> if the chunk is the last which contains no <see cref="value"/> else <see langword="false"/></returns>
    public async Task<bool> GetDoneAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "done");
    }
}
