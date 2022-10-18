using KristofferStrube.Blazor.Streams;
using KristofferStrube.Blazor.Streams.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.FileSystemAccess;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#rs">Streams browser specs</see>
/// </summary>
public class ReadableStream : IAsyncDisposable
{
    protected readonly Lazy<Task<IJSObjectReference>> helperTask;
    protected readonly IJSObjectReference jSInstance;
    private readonly IJSRuntime jSRuntime;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a ReadableStream.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="jSInstance">An JS reference to an existing ReadableStream.</param>
    public static ReadableStream Create(IJSRuntime jSRuntime, IJSObjectReference jSInstance)
    {
        return new ReadableStream(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="underlyingSource">A JS reference to an object equivalent to a <see href="https://streams.spec.whatwg.org/#dictdef-underlyingsource">JS UnderlyingSource</see>.</param>
    /// <param name="strategy">A queing strategy that specifies the chunk size and a high water mark.</param>
    /// <returns>A wrapper instance for a ReadableStream.</returns>
    public static async Task<ReadableStream> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference? underlyingSource = null, QueingStrategy? strategy = null)
    {
        var helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructReadableStream", underlyingSource, strategy);
        return new ReadableStream(jSRuntime, jSInstance);
    }

    internal ReadableStream(IJSRuntime jSRuntime, IJSObjectReference jSInstance)
    {
        helperTask = new(() => jSRuntime.GetHelperAsync());
        this.jSInstance = jSInstance;
        this.jSRuntime = jSRuntime;
    }

    public async Task<bool> GetLockedAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", jSInstance, "locked");
    }

    public async Task CancelAsync()
    {
        await jSInstance.InvokeVoidAsync("cancel");
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
