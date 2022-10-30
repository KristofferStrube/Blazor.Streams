using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#readablestreamdefaultreader">Streams browser specs</see>
/// </summary>
public class ReadableStreamDefaultReaderInProcess : ReadableStreamDefaultReader
{
    public new IJSInProcessObjectReference JSReference;
    protected readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStreamDefaultReader"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamDefaultReader"/>.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableStreamDefaultReaderInProcess"/>.</returns>
    public static async Task<ReadableStreamDefaultReaderInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
        return new ReadableStreamDefaultReaderInProcess(jSRuntime, inProcesshelper, jSReference);
    }

    /// <summary>
    /// Constructs a <see cref="ReadableStreamDefaultReaderInProcess"/> from some <see cref="ReadableStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="stream">A <see cref="ReadableStream"/> wrapper instance.</param>
    /// <returns></returns>
    public static new async Task<ReadableStreamDefaultReaderInProcess> CreateAsync(IJSRuntime jSRuntime, ReadableStream stream)
    {
        IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
        IJSInProcessObjectReference jSInstance = inProcesshelper.Invoke<IJSInProcessObjectReference>("constructReadableStreamDefaultReader", stream.JSReference);
        return new ReadableStreamDefaultReaderInProcess(jSRuntime, inProcesshelper, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStreamDefaultReader"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in process helper instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamDefaultReader"/>.</param>
    internal ReadableStreamDefaultReaderInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        this.inProcessHelper = inProcessHelper;
        JSReference = jSReference;
    }

    /// <summary>
    /// Reads a chunk of a stream.
    /// </summary>
    /// <returns>The next chunk of the underlying <see cref="ReadableStream"/>.</returns>
    public new async Task<ReadableStreamReadResultInProcess> ReadAsync()
    {
        IJSInProcessObjectReference jSInstance = await JSReference.InvokeAsync<IJSInProcessObjectReference>("read");
        return new ReadableStreamReadResultInProcess(jSRuntime, inProcessHelper, jSInstance);
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
