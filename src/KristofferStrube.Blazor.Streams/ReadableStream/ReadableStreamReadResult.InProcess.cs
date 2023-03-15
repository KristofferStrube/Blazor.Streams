using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-readablestreamreadresult">Streams browser specs</see>
/// </summary>
public class ReadableStreamReadResultInProcess : ReadableStreamReadResult
{
    private readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStreamReadResult"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in process helper instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamReadResult"/>.</param>
    internal ReadableStreamReadResultInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        this.inProcessHelper = inProcessHelper;
    }

    /// <summary>
    /// A JS Reference to a chunk of data.
    /// </summary>
    /// <returns>A <see cref="IJSObjectReference"/> to a value which can be of <c>any</c> type.</returns>
    public IJSObjectReference Value => inProcessHelper.Invoke<IJSObjectReference>("getAttribute", JSReference, "value");

    /// <summary>
    /// Indicates whether this is the last read which means that <see cref="Value"/> will be <c>undefined</c>.
    /// </summary>
    /// <returns><see langword="true"/> if the chunk is the last which contains no <see cref="Value"/> else <see langword="false"/></returns>
    public bool Done => inProcessHelper.Invoke<bool>("getAttribute", JSReference, "done");
}
