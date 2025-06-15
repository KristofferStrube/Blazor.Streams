using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams
{
    /// <summary>
    /// <see href="https://streams.spec.whatwg.org/#dictdef-readablewritablepair">Streams browser specs</see>
    /// </summary>
    public class ReadableWritablePairInProcess : ReadableWritablePair, IGenericTransformStreamInProcess, IJSInProcessCreatable<ReadableWritablePairInProcess, ReadableWritablePair>
    {
        /// <summary>
        /// An in-process helper module instance from the Blazor.Streams library.
        /// </summary>
        protected readonly IJSInProcessObjectReference inProcessHelper;

        /// <inheritdoc/>
        public new IJSInProcessObjectReference JSReference { get; }

        /// <inheritdoc/>
        public static async Task<ReadableWritablePairInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
        {
            return await CreateAsync(jSRuntime, jSReference, new());
        }

        /// <inheritdoc/>
        public static async Task<ReadableWritablePairInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference, CreationOptions options)
        {
            IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
            return new ReadableWritablePairInProcess(jSRuntime, inProcesshelper, jSReference, options);
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
            return new ReadableWritablePairInProcess(jSRuntime, inProcesshelper, jSInstance, new() { DisposesJSReference = true });
        }

        /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSInProcessObjectReference, CreationOptions)"/>
        protected ReadableWritablePairInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
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
                return new ReadableStreamInProcess(JSRuntime, inProcessHelper, jSInstance, new() { DisposesJSReference = true });
            }
            set => inProcessHelper.InvokeVoid("setAttribute", JSReference, "readable", value.JSReference);
        }

        [Obsolete("This will be removed in the next major release as all creator methods should be asynchronous for uniformity. Use GetWritableAsync instead.")]
        public WritableStreamInProcess Writable
        {
            get
            {
                IJSInProcessObjectReference jSInstance = inProcessHelper.Invoke<IJSInProcessObjectReference>("getAttribute", JSReference, "writable");
                return new WritableStreamInProcess(JSRuntime, inProcessHelper, jSInstance, new() { DisposesJSReference = true });
            }
            set => inProcessHelper.InvokeVoid("setAttribute", JSReference, "writable", value.JSReference);
        }

        IJSInProcessObjectReference IJSInProcessCreatable<ReadableWritablePairInProcess, ReadableWritablePair>.JSReference => throw new NotImplementedException();

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
}
