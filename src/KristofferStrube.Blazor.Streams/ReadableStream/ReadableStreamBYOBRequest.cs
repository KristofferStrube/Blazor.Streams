using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#readablestreambyobrequest">Streams browser specs</see>
/// </summary>
public class ReadableStreamBYOBRequest : IAsyncDisposable
{
    public readonly IJSObjectReference JSReference;
    protected readonly Lazy<Task<IJSObjectReference>> helperTask;
    protected readonly IJSRuntime jSRuntime;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStreamBYOBRequest"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamBYOBRequest"/>.</param>
    internal ReadableStreamBYOBRequest(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        helperTask = new(() => jSRuntime.GetHelperAsync());
        JSReference = jSReference;
        this.jSRuntime = jSRuntime;
    }

    public async Task<ArrayBufferView?> GetViewAsync()
    {
        var helper = await helperTask.Value;
        var jSInstance = await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "view");
        if (jSInstance is null)
        {
            return null;
        }
        return new ArrayBufferView(jSInstance);
    }

    public async Task RespondAsync(ulong bytesWritten)
    {
        await JSReference.InvokeVoidAsync("respond", bytesWritten);
    }

    public async Task RespondWithNewViewAsync(ArrayBufferView view)
    {
        await JSReference.InvokeVoidAsync("respondWithNewView", view.JSReference);
    }

    public async ValueTask DisposeAsync()
    {
        if (helperTask.IsValueCreated)
        {
            IJSObjectReference module = await helperTask.Value;
            await module.DisposeAsync();
        }
        GC.SuppressFinalize(this);
    }
}
