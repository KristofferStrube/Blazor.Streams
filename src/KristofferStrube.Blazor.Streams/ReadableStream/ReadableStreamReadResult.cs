using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-readablestreamreadresult">Streams browser specs</see>
/// </summary>
public class ReadableStreamReadResult : BaseJSWrapper, IJSCreatable<ReadableStreamReadResult>
{
    /// <inheritdoc/>
    public static async Task<ReadableStreamReadResult> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static Task<ReadableStreamReadResult> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new ReadableStreamReadResult(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected internal ReadableStreamReadResult(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }

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
    /// <returns><see langword="true"/> if the chunk is the last which contains no <see cref="GetValueAsync"/> else <see langword="false"/></returns>
    public async Task<bool> GetDoneAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "done");
    }
}
