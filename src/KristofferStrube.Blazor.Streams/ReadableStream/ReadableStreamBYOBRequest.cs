using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#readablestreambyobrequest">Streams browser specs</see>
/// </summary>
public class ReadableStreamBYOBRequest : BaseJSWrapper, IJSCreatable<ReadableStreamBYOBRequest>
{
    /// <inheritdoc/>
    public static async Task<ReadableStreamBYOBRequest> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static Task<ReadableStreamBYOBRequest> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new ReadableStreamBYOBRequest(jSRuntime, jSReference, options));
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected internal ReadableStreamBYOBRequest(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }

    /// <summary>
    /// The view that should be written to. Is null if it has been responded already.
    /// </summary>
    /// <returns>A new concrete implementation of an <see cref="IArrayBufferView"/>.</returns>
    public async Task<IArrayBufferView?> GetViewAsync()
    {
        ValueReference viewAttribute = new(JSRuntime, JSReference, "view");

        viewAttribute.ValueMapper = new()
        {
            ["float32array"] = async () => await viewAttribute.GetCreatableAsync<Float32Array>(),
            ["uint8array"] = async () => await viewAttribute.GetCreatableAsync<Uint8Array>(),
            ["uint16array"] = async () => await viewAttribute.GetCreatableAsync<Uint16Array>(),
            ["uint32array"] = async () => await viewAttribute.GetCreatableAsync<Uint32Array>()
        };

        var value = await viewAttribute.GetValueAsync();

        if (value is not IArrayBufferView { } arrayBufferView)
        {
            var typeName = await viewAttribute.GetTypeNameAsync();
            throw new NotSupportedException($"The type of view '{typeName}' is not supported. If you need to use this you can request support for it in the Blazor.WebIDL library.");
        }

        return arrayBufferView;
    }

    /// <summary>
    /// Should be called after having written to the the view.
    /// </summary>
    /// <param name="bytesWritten">The number of bytes that had been written to the view.</param>
    /// <returns></returns>
    public async Task RespondAsync(ulong bytesWritten)
    {
        await JSReference.InvokeVoidAsync("respond", bytesWritten);
    }

    /// <summary>
    /// Indicates that there was supplied a new <see cref="IArrayBufferView"/> as the source for the write.
    /// </summary>
    /// <param name="view">A new view. The constraints for what this can be are extensive, so look into the documentation if you need this.</param>
    public async Task RespondWithNewViewAsync(IArrayBufferView view)
    {
        await JSReference.InvokeVoidAsync("respondWithNewView", view.JSReference);
    }
}
