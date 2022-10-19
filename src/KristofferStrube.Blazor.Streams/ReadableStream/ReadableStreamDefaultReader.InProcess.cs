using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#readablestreamdefaultreader">Streams browser specs</see>
/// </summary>
public class ReadableStreamDefaultReaderInProcess : ReadableStreamReaderInProcess
{
    /// <summary>
    /// Constructs a <see cref="ReadableStreamDefaultReaderInProcess"/> from some <see cref="ReadableStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="stream">A <see cref="ReadableStream"/> wrapper instance.</param>
    /// <returns></returns>
    public static ReadableStreamDefaultReaderInProcess Create(IJSRuntime jSRuntime, ReadableStream stream)
    {
        IJSInProcessObjectReference inProcesshelper = jSRuntime.GetInProcessHelper();
        IJSObjectReference jSInstance = inProcesshelper.Invoke<IJSObjectReference>("constructReadableStreamDefaultReader", stream.JSReference);
        return new ReadableStreamDefaultReaderInProcess(jSRuntime, inProcesshelper, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStreamDefaultReader"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in process helper instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamDefaultReader"/>.</param>
    internal ReadableStreamDefaultReaderInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSObjectReference jSReference) : base(jSRuntime, inProcessHelper, jSReference) { }

    /// <summary>
    /// Reads a chunk of a stream.
    /// </summary>
    /// <returns>The next chunk of the underlying <see cref="ReadableStream"/>.</returns>
    public ReadableStreamReadResultInProcess Read()
    {
        IJSObjectReference jSInstance = ((IJSInProcessObjectReference)JSReference).Invoke<IJSObjectReference>("read");
        return new ReadableStreamReadResultInProcess(jSRuntime, inProcessHelper, jSInstance);
    }
}
