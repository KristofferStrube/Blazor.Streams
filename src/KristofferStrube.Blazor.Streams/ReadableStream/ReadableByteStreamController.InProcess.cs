using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams
{
    /// <summary>
    /// <see href="https://streams.spec.whatwg.org/#rbs-controller-class">Streams browser specs</see>
    /// </summary>
    public class ReadableByteStreamControllerInProcess : ReadableByteStreamController, IJSInProcessCreatable<ReadableByteStreamControllerInProcess, ReadableByteStreamController>
    {
        /// <summary>
        /// An in-process helper module instance from the Blazor.Streams library.
        /// </summary>
        protected readonly IJSInProcessObjectReference inProcessHelper;

        /// <inheritdoc/>
        public new IJSInProcessObjectReference JSReference { get; }

        /// <inheritdoc/>
        public static async Task<ReadableByteStreamControllerInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
        {
            return await CreateAsync(jSRuntime, jSReference, new());
        }

        /// <inheritdoc/>
        public static async Task<ReadableByteStreamControllerInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference, CreationOptions options)
        {
            IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
            return new ReadableByteStreamControllerInProcess(jSRuntime, inProcesshelper, jSReference, options);
        }

        /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSInProcessObjectReference, CreationOptions)"/>
        protected internal ReadableByteStreamControllerInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
        {
            this.inProcessHelper = inProcessHelper;
            JSReference = jSReference;
        }

        /// <summary>
        /// Returns the current BYOB pull request, or null if there isn’t one.
        /// </summary>
        /// <returns>A <see cref="ReadableStreamBYOBRequestInProcess"/></returns>
        public ReadableStreamBYOBRequestInProcess? BYOBRequest
        {
            get
            {
                IJSInProcessObjectReference? jSInstance = inProcessHelper.Invoke<IJSInProcessObjectReference?>("getAttribute", JSReference, "byobRequest");
                if (jSInstance is null)
                {
                    return null;
                }
                return new ReadableStreamBYOBRequestInProcess(JSRuntime, inProcessHelper, jSInstance, new() { DisposesJSReference = true });
            }
        }

        /// <summary>
        /// The desired size to fill the controlled stream's internal queue.
        /// </summary>
        /// <returns>Negative values means that the queue is overfull.</returns>
        public double? DesiredSize => inProcessHelper.Invoke<double?>("getAttribute", JSReference, "desiredSize");

        /// <summary>
        /// Closes the controlled stream once all previously enqueued chunks have been read.
        /// </summary>
        public void Close()
        {
            JSReference.InvokeVoid("close");
        }

        /// <summary>
        /// Enqueues the chunk in the controlled stream.
        /// </summary>
        /// <param name="chunk">An <see cref="IArrayBufferView"/> supplied as the BYOB.</param>
        public void Enqueue(IArrayBufferView chunk)
        {
            JSReference.InvokeVoid("enqueue", chunk.JSReference);
        }

        /// <summary>
        /// Errors the controlled stream so that all future interactions will fail.
        /// </summary>
        public void Error()
        {
            JSReference.InvokeVoid("error");
        }
    }
}
