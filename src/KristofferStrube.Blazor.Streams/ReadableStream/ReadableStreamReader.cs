using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#typedefdef-readablestreamreader">Streams browser specs</see>
/// </summary>
public abstract class ReadableStreamReader : IAsyncDisposable
{
    public readonly IJSObjectReference JSReference;
    protected readonly Lazy<Task<IJSObjectReference>> helperTask;
    protected readonly IJSRuntime jSRuntime;

    /// <summary>
    /// Constructs a wrapper instance for a given JS instance of a <see cref="ReadableStreamReader"/>.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamReader"/>.</param>
    internal ReadableStreamReader(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        helperTask = new(() => jSRuntime.GetHelperAsync());
        JSReference = jSReference;
        this.jSRuntime = jSRuntime;
    }

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

    public async ValueTask DisposeAsync()
    {
        await ReleaseLockAsync();
        await JSReference.DisposeAsync();
        if (helperTask.IsValueCreated)
        {
            IJSObjectReference module = await helperTask.Value;
            await module.DisposeAsync();
        }
        GC.SuppressFinalize(this);
    }
}
