using Microsoft.JSInterop;
using System.Linq.Expressions;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#rs">Streams browser specs</see>
/// </summary>
public class ReadableStreamInProcess : ReadableStream
{
    public new IJSInProcessObjectReference JSReference;
    private readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a ReadableStream.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="jSInstance">An JS reference to an existing ReadableStream.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableStreamInProcess"/> which can access attributes synchronously.</returns>
    public static async Task<ReadableStreamInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSInstance)
    {
        IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
        return new ReadableStreamInProcess(jSRuntime, inProcesshelper, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="underlyingSource">A JS reference to an object equivalent to a <see href="https://streams.spec.whatwg.org/#dictdef-underlyingsource">JS UnderlyingSource</see>.</param>
    /// <param name="strategy">A queing strategy that specifies the chunk size and a high water mark.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableStreamInProcess"/> which can access attributes synchronously.</returns>
    public static new async Task<ReadableStreamInProcess> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference? underlyingSource = null, QueingStrategy? strategy = null)
    {
        IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
        IJSInProcessObjectReference jSInstance = inProcesshelper.Invoke<IJSInProcessObjectReference>("constructReadableStream", underlyingSource, strategy);
        return new ReadableStreamInProcess(jSRuntime, inProcesshelper, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStreamInProcess"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in process helper instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamInProcess"/>.</param>
    internal ReadableStreamInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        this.inProcessHelper = inProcessHelper;
        JSReference = jSReference;
    }

    /// <summary>
    /// Indicates whether the stream already has a reader.
    /// </summary>
    /// <returns><see langword="true"/> if the internal reader is a <see cref="ReadableStreamDefaultReader"/> or a <see cref="ReadableStreamBYOBReader"/> and returns <see langword="false"/> else meaning that the internal reader is <c>undefined</c></returns>
    public bool Locked => inProcessHelper.Invoke<bool>("getAttribute", JSReference, "locked");

    /// <summary>
    /// Closes the internal reader if it is locked.
    /// </summary>
    /// <returns></returns>
    public void Cancel()
    {
        ((IJSInProcessObjectReference)JSReference).InvokeVoid("cancel");
    }

    /// <summary>
    /// Creates a new <see cref="ReadableStreamReader"/> that it assigns to the internal <c>reader</c> slot and returns that.
    /// </summary>
    /// <param name="options">Options that can be used to indicate that a <see cref="ReadableStreamBYOBReader"/> should be created.</param>
    /// <returns>If <paramref name="options"/> is <see langword="not"/> <see langword="null"/> and the <see cref="ReadableStreamGetReaderOptions.Mode"/> is <see cref="ReadableStreamReaderMode.Byob"/> it returns a <see cref="ReadableStreamBYOBReaderInProcess"/> else it returns a <see cref="ReadableStreamDefaultReaderInProcess"/>.</returns>
    public ReadableStreamReaderInProcess GetReader(ReadableStreamGetReaderOptions? options = null)
    {
        IJSInProcessObjectReference jSInstance = JSReference.Invoke<IJSInProcessObjectReference>("getReader", options);
        if (options?.Mode is ReadableStreamReaderMode.Byob)
        {
            return new ReadableStreamBYOBReaderInProcess(jSRuntime, inProcessHelper, jSInstance);
        }
        else
        {
            return new ReadableStreamDefaultReaderInProcess(jSRuntime, inProcessHelper, jSInstance);
        }
    }
}
