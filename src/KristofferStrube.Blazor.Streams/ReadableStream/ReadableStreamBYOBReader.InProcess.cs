using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#readablestreambyobreader">Streams browser specs</see>
/// </summary>
public class ReadableStreamBYOBReaderInProcess : ReadableStreamBYOBReader, IJSInProcessCreatable<ReadableStreamBYOBReaderInProcess, ReadableStreamBYOBReader>
{
    /// <summary>
    /// An in-process helper module instance from the Blazor.Streams library.
    /// </summary>
    protected readonly IJSInProcessObjectReference inProcessHelper;

    /// <inheritdoc/>
    public new IJSInProcessObjectReference JSReference { get; }

    /// <inheritdoc/>
    public static async Task<ReadableStreamBYOBReaderInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static async Task<ReadableStreamBYOBReaderInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference, CreationOptions options)
    {
        IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
        return new ReadableStreamBYOBReaderInProcess(jSRuntime, inProcesshelper, jSReference, options);
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSInProcessObjectReference, CreationOptions)"/>
    public static new async Task<ReadableStreamBYOBReaderInProcess> CreateAsync(IJSRuntime jSRuntime, ReadableStream stream)
    {
        IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
        IJSInProcessObjectReference jSInstance = inProcesshelper.Invoke<IJSInProcessObjectReference>("constructReadableStreamBYOBReader", stream.JSReference);
        return new ReadableStreamBYOBReaderInProcess(jSRuntime, inProcesshelper, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSInProcessObjectReference, CreationOptions)"/>
    internal ReadableStreamBYOBReaderInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
        this.inProcessHelper = inProcessHelper;
        JSReference = jSReference;
    }

    /// <summary>
    /// Reads a chunk of a stream.
    /// </summary>
    /// <param name="view">The <see cref="IArrayBufferView"/> that is used as a buffer</param>
    /// <returns>The next chunk of the underlying <see cref="ReadableStream"/>.</returns>
    public new async Task<ReadableStreamReadResultInProcess> ReadAsync(IArrayBufferView view)
    {
        IJSInProcessObjectReference jSInstance = await JSReference.InvokeAsync<IJSInProcessObjectReference>("read", view.JSReference);
        return new ReadableStreamReadResultInProcess(JSRuntime, inProcessHelper, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Reads a chunk of a stream.
    /// </summary>
    /// <param name="view">The <see cref="IArrayBufferView"/> that is used as a buffer</param>
    /// <param name="options">The options for how the chunk is to be read.</param>
    /// <returns>The next chunk of the underlying <see cref="ReadableStream"/>.</returns>
    public new async Task<ReadableStreamReadResultInProcess> ReadAsync(IArrayBufferView view, ReadableStreamBYOBReaderReadOptions? options = null)
    {
        IJSInProcessObjectReference jSInstance = await JSReference.InvokeAsync<IJSInProcessObjectReference>("read", view.JSReference, options);
        return new ReadableStreamReadResultInProcess(JSRuntime, inProcessHelper, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Sets the internal <c>reader</c> slot to <c>undefined</c>.
    /// </summary>
    /// <returns></returns>
    public void ReleaseLock()
    {
        JSReference.InvokeVoid("releaseLock");
    }

    /// <summary>
    /// Gets a JS reference to the closed attribute.
    /// </summary>
    /// <returns></returns>
    public IJSObjectReference Closed => inProcessHelper.Invoke<IJSObjectReference>("getAttribute", JSReference, "closed");
}
