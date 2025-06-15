using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams
{
    /// <summary>
    /// <see href="https://streams.spec.whatwg.org/#readablestreamdefaultreader">Streams browser specs</see>
    /// </summary>
    public class ReadableStreamDefaultReaderInProcess : ReadableStreamDefaultReader, IJSInProcessCreatable<ReadableStreamDefaultReaderInProcess, ReadableStreamDefaultReader>
    {
        /// <summary>
        /// An in-process helper module instance from the Blazor.Streams library.
        /// </summary>
        protected readonly IJSInProcessObjectReference inProcessHelper;

        /// <inheritdoc/>
        public new IJSInProcessObjectReference JSReference { get; }

        /// <inheritdoc/>
        public static async Task<ReadableStreamDefaultReaderInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
        {
            return await CreateAsync(jSRuntime, jSReference, new());
        }

        /// <inheritdoc/>
        public static async Task<ReadableStreamDefaultReaderInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference, CreationOptions options)
        {
            IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
            return new ReadableStreamDefaultReaderInProcess(jSRuntime, inProcesshelper, jSReference, options);
        }

        /// <summary>
        /// Constructs a <see cref="ReadableStreamDefaultReaderInProcess"/> from some <see cref="ReadableStream"/>.
        /// </summary>
        /// <param name="jSRuntime">An IJSRuntime instance.</param>
        /// <param name="stream">A <see cref="ReadableStream"/> wrapper instance.</param>
        /// <returns>A wrapper instance for a <see cref="ReadableStreamDefaultReader"/>.</returns>
        public static new async Task<ReadableStreamDefaultReaderInProcess> CreateAsync(IJSRuntime jSRuntime, ReadableStream stream)
        {
            IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
            IJSInProcessObjectReference jSInstance = inProcesshelper.Invoke<IJSInProcessObjectReference>("constructReadableStreamDefaultReader", stream.JSReference);
            return new ReadableStreamDefaultReaderInProcess(jSRuntime, inProcesshelper, jSInstance, new() { DisposesJSReference = true });
        }

        /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSInProcessObjectReference, CreationOptions)"/>
        protected internal ReadableStreamDefaultReaderInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
        {
            this.inProcessHelper = inProcessHelper;
            JSReference = jSReference;
        }

        /// <summary>
        /// Reads a chunk of a stream.
        /// </summary>
        /// <returns>The next chunk of the underlying <see cref="ReadableStream"/>.</returns>
        public new async Task<ReadableStreamReadResultInProcess> ReadAsync()
        {
            IJSInProcessObjectReference jSInstance = await JSReference.InvokeAsync<IJSInProcessObjectReference>("read");
            return new ReadableStreamReadResultInProcess(JSRuntime, inProcessHelper, jSInstance, new() { DisposesJSReference = true });
        }

        /// <summary>
        /// Sets the internal <c>reader</c> slot to <c>undefined</c>.
        /// </summary>
        /// <returns></returns>
        public void ReleaseLock()
        {
            JSReference.InvokeVoid("releaseLock");
        }

        /// <summary>
        /// Gets a JS reference to the closed attribute.
        /// </summary>
        /// <returns></returns>
        public IJSObjectReference Closed => inProcessHelper.Invoke<IJSObjectReference>("getAttribute", JSReference, "closed");
    }
}
