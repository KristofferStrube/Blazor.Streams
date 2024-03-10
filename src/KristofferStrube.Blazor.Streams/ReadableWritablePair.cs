using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-readablewritablepair">Streams browser specs</see>
/// </summary>
public class ReadableWritablePair : BaseJSWrapper, IGenericTransformStream, IJSCreatable<ReadableWritablePair>
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableWritablePair"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableWritablePair"/>.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableWritablePair"/>.</returns>
    [Obsolete("This will be removed in the next major release as all creator methods should be asynchronous for uniformity. Use CreateAsync instead.")]
    public static ReadableWritablePair Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return new ReadableWritablePair(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static async Task<ReadableWritablePair> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static Task<ReadableWritablePair> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new ReadableWritablePair(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="readable">The <see cref="ReadableStream"/> part of the pair.</param>
    /// <param name="writable">The <see cref="WritableStream"/> part of the pair.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableWritablePair"/>.</returns>
    public static async Task<ReadableWritablePair> CreateAsync(IJSRuntime jSRuntime, ReadableStream readable, WritableStream writable)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructReadableWritablePair", readable.JSReference, writable.JSReference);
        return new ReadableWritablePair(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected ReadableWritablePair(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }

    public async Task<ReadableStream> GetReadableAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "readable");
        return await ReadableStream.CreateAsync(JSRuntime, jSInstance);
    }

    public async Task<WritableStream> GetWritableAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "writable");
        return await WritableStream.CreateAsync(JSRuntime, jSInstance);
    }
}
