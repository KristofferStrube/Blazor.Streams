using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-readablewritablepair">Streams browser specs</see>
/// </summary>
public class ReadableWritablePairInProcess : ReadableWritablePair
{
    public new IJSInProcessObjectReference JSReference;
    private readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableWritablePair"/>.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="jSInstance">An JS reference to an existing ReadableStream.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableWritablePairInProcess"/> which can access attributes synchronously.</returns>
    public static async Task<ReadableWritablePairInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSInstance)
    {
        IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
        return new ReadableWritablePairInProcess(jSRuntime, inProcesshelper, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableWritablePairInProcess"/> which can access attributes synchronously.</returns>
    public static new async Task<ReadableWritablePairInProcess> CreateAsync(IJSRuntime jSRuntime, ReadableStream readable, WritableStream writable)
    {
        IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
        IJSInProcessObjectReference jSInstance = inProcesshelper.Invoke<IJSInProcessObjectReference>("constructReadableWritablePair", readable, writable);
        return new ReadableWritablePairInProcess(jSRuntime, inProcesshelper, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStreamInProcess"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in process helper instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamInProcess"/>.</param>
    internal ReadableWritablePairInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        this.inProcessHelper = inProcessHelper;
        JSReference = jSReference;
    }

    public ReadableStream Readable
    {
        get
        {
            var jSInstance = inProcessHelper.Invoke<IJSObjectReference>("getAttribute", JSReference, "readable");
            return new ReadableStream(jSRuntime, jSInstance);
        }
        set
        {
            inProcessHelper.InvokeVoid("setAttribute", JSReference, "readable", value.JSReference);
        }
    }

    public WritableStream Writable
    {
        get
        {
            var jSInstance = inProcessHelper.Invoke<IJSObjectReference>("getAttribute", JSReference, "writable");
            return new WritableStream(jSRuntime, jSInstance);
        }
        set
        {
            inProcessHelper.InvokeVoid("setAttribute", JSReference, "writable", value.JSReference);
        }
    }
}
