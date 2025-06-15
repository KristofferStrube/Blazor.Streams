using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams
{
    /// <summary>
    /// <see href="https://streams.spec.whatwg.org/#ws-class-definition">Streams browser specs</see>
    /// </summary>
    public class WritableStreamInProcess : WritableStream, IJSInProcessCreatable<WritableStreamInProcess, WritableStream>
    {
        /// <summary>
        /// An in-process helper module instance from the Blazor.Streams library.
        /// </summary>
        protected readonly IJSInProcessObjectReference inProcessHelper;

        /// <inheritdoc/>
        public new IJSInProcessObjectReference JSReference { get; }

        /// <inheritdoc/>
        public static async Task<WritableStreamInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
        {
            return await CreateAsync(jSRuntime, jSReference, new());
        }

        /// <inheritdoc/>
        public static async Task<WritableStreamInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference, CreationOptions options)
        {
            IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
            return new WritableStreamInProcess(jSRuntime, inProcesshelper, jSReference, options);
        }

        /// <summary>
        /// Constructs a wrapper instance using the standard constructor
        /// </summary>
        /// <param name="jSRuntime">An IJSRuntime instance.</param>
        /// <param name="underlyingSink">An <see cref="UnderlyingSink"/> that which implements the Start, Write, Close, and/or Abort methods.</param>
        /// <param name="strategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <returns>A wrapper instance for a <see cref="WritableStream"/>.</returns>
        public static new async Task<WritableStreamInProcess> CreateAsync(IJSRuntime jSRuntime, UnderlyingSink? underlyingSink = null, QueuingStrategy? strategy = null)
        {
            return await CreatePrivateAsync(jSRuntime, underlyingSink, strategy);
        }

        /// <summary>
        /// Constructs a wrapper instance using the standard constructor
        /// </summary>
        /// <param name="jSRuntime">An IJSRuntime instance.</param>
        /// <param name="underlyingSink">An <see cref="UnderlyingSink"/> that which implements the Start, Write, Close, and/or Abort methods.</param>
        /// <param name="strategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <returns>A wrapper instance for a <see cref="WritableStream"/>.</returns>
        public static new async Task<WritableStreamInProcess> CreateAsync(IJSRuntime jSRuntime, UnderlyingSink? underlyingSink = null, ByteLengthQueuingStrategy? strategy = null)
        {
            return await CreatePrivateAsync(jSRuntime, underlyingSink, strategy?.JSReference);
        }

        /// <summary>
        /// Constructs a wrapper instance using the standard constructor
        /// </summary>
        /// <param name="jSRuntime">An IJSRuntime instance.</param>
        /// <param name="underlyingSink">An <see cref="UnderlyingSink"/> that which implements the Start, Write, Close, and/or Abort methods.</param>
        /// <param name="strategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <returns>A wrapper instance for a <see cref="WritableStream"/>.</returns>
        public static new async Task<WritableStreamInProcess> CreateAsync(IJSRuntime jSRuntime, UnderlyingSink? underlyingSink = null, CountQueuingStrategy? strategy = null)
        {
            return await CreatePrivateAsync(jSRuntime, underlyingSink, strategy?.JSReference);
        }

        private static async Task<WritableStreamInProcess> CreatePrivateAsync(IJSRuntime jSRuntime, object? underlyingSink = null, object? strategy = null)
        {
            IJSInProcessObjectReference inProcessHelper = await jSRuntime.GetInProcessHelperAsync();
            IJSInProcessObjectReference jSInstance = await inProcessHelper.InvokeAsync<IJSInProcessObjectReference>("constructWritableStream", underlyingSink, strategy);
            return new WritableStreamInProcess(jSRuntime, inProcessHelper, jSInstance, new() { DisposesJSReference = true });
        }

        /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSInProcessObjectReference, CreationOptions)"/>
        protected internal WritableStreamInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
        {
            this.inProcessHelper = inProcessHelper;
            JSReference = jSReference;
        }

        /// <summary>
        /// Indicates whether the stream already has a writer.
        /// </summary>
        /// <returns><see langword="false"/> if the internal <c>writer</c> slot is <c>undefined</c> else return <see langword="false"/></returns>
        public bool Locked => inProcessHelper.Invoke<bool>("getAttribute", JSReference, "locked");


        /// <summary>
        /// Creates a new <see cref="WritableStreamDefaultWriter"/> that it assigns to the internal <c>writer</c> slot and returns that.
        /// </summary>
        /// <returns>A new <see cref="WritableStreamDefaultWriter"/>.</returns>
        public WritableStreamDefaultWriterInProcess GetWriter()
        {
            IJSInProcessObjectReference jSInstance = JSReference.Invoke<IJSInProcessObjectReference>("getWriter");
            return new WritableStreamDefaultWriterInProcess(JSRuntime, inProcessHelper, jSInstance, new() { DisposesJSReference = true });
        }
    }
}
