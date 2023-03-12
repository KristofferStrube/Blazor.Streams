using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-readablewritablepair">Streams browser specs</see>
/// </summary>
public class ReadableWritablePair : BaseJSWrapper, IGenericTransformStream
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableWritablePair"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableWritablePair"/>.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableWritablePair"/>.</returns>
    public static ReadableWritablePair Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return new ReadableWritablePair(jSRuntime, jSReference);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableWritablePair"/>.</returns>
    public static async Task<ReadableWritablePair> CreateAsync(IJSRuntime jSRuntime, ReadableStream readable, WritableStream writable)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructReadableWritablePair", readable.JSReference, writable.JSReference);
        return new ReadableWritablePair(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableWritablePair"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableWritablePair"/>.</param>
    internal ReadableWritablePair(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task<ReadableStream> GetReadableAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "readable");
        return new ReadableStream(JSRuntime, jSInstance);
    }

    public async Task<WritableStream> GetWritableAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "writable");
        return WritableStream.Create(JSRuntime, jSInstance);
    }
}
