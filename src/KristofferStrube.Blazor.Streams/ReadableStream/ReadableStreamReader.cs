using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#typedefdef-readablestreamreader">Streams browser specs</see>
/// </summary>
public abstract class ReadableStreamReader : BaseJSWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS instance of a <see cref="ReadableStreamReader"/>.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamReader"/>.</param>
    protected ReadableStreamReader(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// Sets the internal <c>reader</c> slot to <c>undefined</c>.
    /// </summary>
    /// <returns></returns>
    public async Task ReleaseLockAsync()
    {
        await JSReference.InvokeVoidAsync("releaseLock");
    }

    /// <summary>
    /// Gets a JS reference to the closed attribute.
    /// </summary>
    /// <returns></returns>
    public async Task<IJSObjectReference> GetClosedAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "closed");
    }

    /// <summary>
    /// Cancels the underlying stream which is equivalent to <see cref="ReadableStream.CancelAsync"/>
    /// </summary>
    /// <returns></returns>
    public async Task CancelAsync()
    {
        await JSReference.InvokeVoidAsync("cancel");
    }
}
