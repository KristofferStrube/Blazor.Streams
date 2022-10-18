using KristofferStrube.Blazor.Streams;
using KristofferStrube.Blazor.Streams.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.FileSystemAccess;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#rs">Streams browser specs</see>
/// </summary>
public class ReadableStreamInProcess : ReadableStream
{
    private readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a ReadableStream.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="jSInstance">An JS reference to an existing ReadableStream.</param>
    public static ReadableStreamInProcess Create(IJSRuntime jSRuntime, IJSObjectReference jSInstance)
    {
        var inProcesshelper = jSRuntime.GetInProcessHelper();
        return new ReadableStreamInProcess(jSRuntime, inProcesshelper, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="underlyingSource">A JS reference to an object equivalent to a <see href="https://streams.spec.whatwg.org/#dictdef-underlyingsource">JS UnderlyingSource</see>.</param>
    /// <param name="strategy">A queing strategy that specifies the chunk size and a high water mark.</param>
    /// <returns>A wrapper instance for a ReadableStream.</returns>
    public static ReadableStreamInProcess Create(IJSRuntime jSRuntime, IJSObjectReference? underlyingSource = null, QueingStrategy? strategy = null)
    {
        var inProcesshelper = jSRuntime.GetInProcessHelper();
        IJSObjectReference jSInstance = inProcesshelper.Invoke<IJSObjectReference>("constructReadableStream", underlyingSource, strategy);
        return new ReadableStreamInProcess(jSRuntime, inProcesshelper, jSInstance);
    }

    internal ReadableStreamInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSObjectReference jSInstance) : base(jSRuntime, jSInstance)
    {
        this.inProcessHelper = inProcessHelper;
    }

    public bool Locked => inProcessHelper.Invoke<bool>("getAttribute", jSInstance, "locked");
}
