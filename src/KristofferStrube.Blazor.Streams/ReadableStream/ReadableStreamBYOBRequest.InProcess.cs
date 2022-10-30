using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#readablestreambyobrequest">Streams browser specs</see>
/// </summary>
public class ReadableStreamBYOBRequestInProcess : ReadableStreamBYOBRequest
{
    public new IJSInProcessObjectReference JSReference;
    private readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStreamBYOBRequest"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in process helper instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamBYOBRequest"/>.</param>
    internal ReadableStreamBYOBRequestInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        this.inProcessHelper = inProcessHelper;
        JSReference = jSReference;
    }

    /// <summary>
    /// The view that should be written to. Is null if it has been responded already.
    /// </summary>
    /// <returns>A shallow <see cref="ArrayBufferView"/> wrapper.</returns>
    public ArrayBufferView? View
    {
        get
        {
            IJSObjectReference? jSInstance = inProcessHelper.Invoke<IJSObjectReference?>("getAttribute", JSReference, "view");
            if (jSInstance is null)
            {
                return null;
            }
            return new ArrayBufferView(jSInstance);
        }
    }

    /// <summary>
    /// Should be called after having written to the the view.
    /// </summary>
    /// <param name="bytesWritten">The number of bytes that had been written to the view.</param>
    /// <returns></returns>
    public void Respond(ulong bytesWritten)
    {
        JSReference.InvokeVoid("respond", bytesWritten);
    }

    /// <summary>
    /// Indicates that there was supplied a new <see cref="ArrayBufferView"/> as the source for the write.
    /// </summary>
    /// <param name="view">A new view. The constraints for what this can be are extensive, so look into the documentation if you need this.</param>
    /// <returns></returns>
    public void RespondWithNewView(ArrayBufferView view)
    {
        JSReference.InvokeVoid("respondWithNewView", view.JSReference);
    }
}
