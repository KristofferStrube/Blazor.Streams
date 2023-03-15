using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-readablewritablepair">Streams browser specs</see>
/// </summary>
public class ReadableWritablePairInProcess : ReadableWritablePair, IGenericTransformStreamInProcess
{
    public new IJSInProcessObjectReference JSReference;
    private readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableWritablePair"/>.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="jSReference">An JS reference to an existing ReadableStream.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableWritablePair"/>.</returns>
    public static async Task<ReadableWritablePairInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
        return new ReadableWritablePairInProcess(jSRuntime, inProcesshelper, jSReference);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="readable">The <see cref="ReadableStream"/> part of the pair.</param>
    /// <param name="writable">The <see cref="WritableStream"/> part of the pair.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableWritablePair"/>.</returns>
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
    protected ReadableWritablePairInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        this.inProcessHelper = inProcessHelper;
        JSReference = jSReference;
    }

    [Obsolete("This will be removed in the next major release as all creator methods should be asynchronous for uniformity. Use GetReadableAsync instead.")]
    public ReadableStreamInProcess Readable
    {
        get
        {
            IJSInProcessObjectReference jSInstance = inProcessHelper.Invoke<IJSInProcessObjectReference>("getAttribute", JSReference, "readable");
            return new ReadableStreamInProcess(JSRuntime, inProcessHelper, jSInstance);
        }
        set => inProcessHelper.InvokeVoid("setAttribute", JSReference, "readable", value.JSReference);
    }

    [Obsolete("This will be removed in the next major release as all creator methods should be asynchronous for uniformity. Use GetWritableAsync instead.")]
    public WritableStreamInProcess Writable
    {
        get
        {
            IJSInProcessObjectReference jSInstance = inProcessHelper.Invoke<IJSInProcessObjectReference>("getAttribute", JSReference, "writable");
            return new WritableStreamInProcess(JSRuntime, inProcessHelper, jSInstance);
        }
        set => inProcessHelper.InvokeVoid("setAttribute", JSReference, "writable", value.JSReference);
    }

    public new async Task<ReadableStreamInProcess> GetReadableAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSInProcessObjectReference jSInstance = await helper.InvokeAsync<IJSInProcessObjectReference>("getAttribute", JSReference, "readable");
        return await ReadableStreamInProcess.CreateAsync(JSRuntime, jSInstance);
    }

    public new async Task<WritableStreamInProcess> GetWritableAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSInProcessObjectReference jSInstance = await helper.InvokeAsync<IJSInProcessObjectReference>("getAttribute", JSReference, "writable");
        return await WritableStreamInProcess.CreateAsync(JSRuntime, jSInstance);
    }
}
