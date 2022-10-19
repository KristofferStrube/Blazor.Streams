using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#readablestreambyobreader">Streams browser specs</see>
/// </summary>
public class ReadableStreamBYOBReaderInProcess : ReadableStreamReaderInProcess
{
    /// <summary>
    /// Constructs a <see cref="ReadableStreamBYOBReaderInProcess"/> from some <see cref="ReadableStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="stream">A <see cref="ReadableStream"/> wrapper instance.</param>
    /// <returns></returns>
    public static ReadableStreamBYOBReaderInProcess Create(IJSRuntime jSRuntime, ReadableStream stream)
    {
        IJSInProcessObjectReference inProcesshelper = jSRuntime.GetInProcessHelper();
        IJSObjectReference jSInstance = inProcesshelper.Invoke<IJSObjectReference>("constructReadableStreamDefaultReader", stream.JSReference);
        return new ReadableStreamBYOBReaderInProcess(jSRuntime, inProcesshelper, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStreamBYOBReader"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in process helper instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamBYOBReader"/>.</param>
    internal ReadableStreamBYOBReaderInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSObjectReference jSReference) : base(jSRuntime, inProcessHelper, jSReference) { }

    /// <summary>
    /// Reads a chunk of a stream.
    /// </summary>
    /// <returns>The next chunk of the underlying <see cref="ReadableStream"/>.</returns>
    public ReadableStreamReadResultInProcess Read(ArrayBufferView view)
    {
        IJSObjectReference jSInstance = inProcessHelper.Invoke<IJSObjectReference>("read", view);
        return new ReadableStreamReadResultInProcess(jSRuntime, inProcessHelper, jSInstance);
    }
}
