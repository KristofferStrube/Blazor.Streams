using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams
{
    /// <summary>
    /// <see href="https://streams.spec.whatwg.org/#readablestreambyobrequest">Streams browser specs</see>
    /// </summary>
    public class ReadableStreamBYOBRequestInProcess : ReadableStreamBYOBRequest, IJSInProcessCreatable<ReadableStreamBYOBRequestInProcess, ReadableStreamBYOBRequest>
    {
        /// <summary>
        /// An in-process helper module instance from the Blazor.Streams library.
        /// </summary>
        protected readonly IJSInProcessObjectReference inProcessHelper;

        /// <inheritdoc/>
        public new IJSInProcessObjectReference JSReference { get; }

        /// <inheritdoc/>
        public static async Task<ReadableStreamBYOBRequestInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
        {
            return await CreateAsync(jSRuntime, jSReference, new());
        }

        /// <inheritdoc/>
        public static async Task<ReadableStreamBYOBRequestInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference, CreationOptions options)
        {
            IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
            return new ReadableStreamBYOBRequestInProcess(jSRuntime, inProcesshelper, jSReference, options);
        }

        /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSInProcessObjectReference, CreationOptions)"/>
        protected internal ReadableStreamBYOBRequestInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
        {
            this.inProcessHelper = inProcessHelper;
            JSReference = jSReference;
        }

        /// <inheritdoc cref="ReadableStreamBYOBRequest.GetViewAsync"/>
        [Obsolete("This will be removed in the next major release. We are unable to get a rich wrapper for an Array buffer synchronously so we shouldn't use this.")]
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

        /// <inheritdoc/>
        public new async Task<IArrayBufferView?> GetViewAsync()
        {
            ValueReferenceInProcess viewAttribute = await ValueReferenceInProcess.CreateAsync(JSRuntime, JSReference, "view");

            viewAttribute.ValueMapper = new()
            {
                ["float32array"] = async () => await viewAttribute.GetCreatableAsync<Float32ArrayInProcess, Float32Array>(),
                ["uint8array"] = async () => await viewAttribute.GetCreatableAsync<Uint8ArrayInProcess, Uint8Array>(),
                ["uint16array"] = async () => await viewAttribute.GetCreatableAsync<Uint16ArrayInProcess, Uint16Array>(),
                ["uint32array"] = async () => await viewAttribute.GetCreatableAsync<Uint32ArrayInProcess, Uint32Array>()
            };

            var value = await viewAttribute.GetValueAsync();

            if (value is not IArrayBufferView { } arrayBufferView)
            {
                var typeName = await viewAttribute.GetTypeNameAsync();
                throw new NotSupportedException($"The type of view '{typeName}' is not supported. If you need to use this you can request support for it in the Blazor.WebIDL library.");
            }

            return arrayBufferView;
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
        /// Indicates that there was supplied a new <see cref="IArrayBufferView"/> as the source for the write.
        /// </summary>
        /// <param name="view">A new view. The constraints for what this can be are extensive, so look into the documentation if you need this.</param>
        public void RespondWithNewView(IArrayBufferView view)
        {
            JSReference.InvokeVoid("respondWithNewView", view.JSReference);
        }
    }
}
