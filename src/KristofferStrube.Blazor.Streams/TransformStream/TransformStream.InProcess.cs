using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams
{
    /// <summary>
    /// <see href="https://streams.spec.whatwg.org/#transformstream">Streams browser specs</see>
    /// </summary>
    public class TransformStreamInProcess : TransformStream, IJSInProcessCreatable<TransformStreamInProcess, TransformStream>
    {
        /// <summary>
        /// An in-process helper module instance from the Blazor.Streams library.
        /// </summary>
        protected readonly IJSInProcessObjectReference inProcessHelper;

        /// <inheritdoc/>
        public new IJSInProcessObjectReference JSReference { get; }

        /// <inheritdoc/>
        public static async Task<TransformStreamInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
        {
            return await CreateAsync(jSRuntime, jSReference, new());
        }

        /// <inheritdoc/>
        public static async Task<TransformStreamInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference, CreationOptions options)
        {
            IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
            return new TransformStreamInProcess(jSRuntime, inProcesshelper, jSReference, options);
        }

        /// <summary>
        /// Constructs a wrapper instance using the standard constructor.
        /// </summary>
        /// <param name="jSRuntime">An IJSRuntime instance.</param>
        /// <param name="transformer">An <see cref="Transformer"/> that which implements the Start, Transform, and/or Cancel methods.</param>
        /// <param name="writableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <param name="readableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <returns>A wrapper instance for a <see cref="TransformStream"/>.</returns>
        public static new async Task<TransformStreamInProcess> CreateAsync(IJSRuntime jSRuntime, Transformer? transformer = null, QueuingStrategy? writableStrategy = null, QueuingStrategy? readableStrategy = null)
        {
            return await CreatePrivateAsync(jSRuntime, transformer, writableStrategy, readableStrategy);
        }

        /// <summary>
        /// Constructs a wrapper instance using the standard constructor.
        /// </summary>
        /// <param name="jSRuntime">An IJSRuntime instance.</param>
        /// <param name="transformer">An <see cref="Transformer"/> that which implements the Start, Transform, and/or Cancel methods.</param>
        /// <param name="writableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <param name="readableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <returns>A wrapper instance for a <see cref="TransformStream"/>.</returns>
        public static new async Task<TransformStreamInProcess> CreateAsync(IJSRuntime jSRuntime, Transformer? transformer = null, QueuingStrategy? writableStrategy = null, ByteLengthQueuingStrategy? readableStrategy = null)
        {
            return await CreatePrivateAsync(jSRuntime, transformer, writableStrategy, readableStrategy?.JSReference);
        }

        /// <summary>
        /// Constructs a wrapper instance using the standard constructor.
        /// </summary>
        /// <param name="jSRuntime">An IJSRuntime instance.</param>
        /// <param name="transformer">An <see cref="Transformer"/> that which implements the Start, Transform, and/or Cancel methods.</param>
        /// <param name="writableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <param name="readableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <returns>A wrapper instance for a <see cref="TransformStream"/>.</returns>
        public static new async Task<TransformStreamInProcess> CreateAsync(IJSRuntime jSRuntime, Transformer? transformer = null, QueuingStrategy? writableStrategy = null, CountQueuingStrategy? readableStrategy = null)
        {
            return await CreatePrivateAsync(jSRuntime, transformer, writableStrategy, readableStrategy?.JSReference);
        }

        /// <summary>
        /// Constructs a wrapper instance using the standard constructor.
        /// </summary>
        /// <param name="jSRuntime">An IJSRuntime instance.</param>
        /// <param name="transformer">An <see cref="Transformer"/> that which implements the Start, Transform, and/or Cancel methods.</param>
        /// <param name="writableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <param name="readableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <returns>A wrapper instance for a <see cref="TransformStream"/>.</returns>
        public static new async Task<TransformStreamInProcess> CreateAsync(IJSRuntime jSRuntime, Transformer? transformer = null, ByteLengthQueuingStrategy? writableStrategy = null, QueuingStrategy? readableStrategy = null)
        {
            return await CreatePrivateAsync(jSRuntime, transformer, writableStrategy?.JSReference, readableStrategy);
        }

        /// <summary>
        /// Constructs a wrapper instance using the standard constructor.
        /// </summary>
        /// <param name="jSRuntime">An IJSRuntime instance.</param>
        /// <param name="transformer">An <see cref="Transformer"/> that which implements the Start, Transform, and/or Cancel methods.</param>
        /// <param name="writableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <param name="readableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <returns>A wrapper instance for a <see cref="TransformStream"/>.</returns>
        public static new async Task<TransformStreamInProcess> CreateAsync(IJSRuntime jSRuntime, Transformer? transformer = null, ByteLengthQueuingStrategy? writableStrategy = null, ByteLengthQueuingStrategy? readableStrategy = null)
        {
            return await CreatePrivateAsync(jSRuntime, transformer, writableStrategy?.JSReference, readableStrategy?.JSReference);
        }

        /// <summary>
        /// Constructs a wrapper instance using the standard constructor.
        /// </summary>
        /// <param name="jSRuntime">An IJSRuntime instance.</param>
        /// <param name="transformer">An <see cref="Transformer"/> that which implements the Start, Transform, and/or Cancel methods.</param>
        /// <param name="writableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <param name="readableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <returns>A wrapper instance for a <see cref="TransformStream"/>.</returns>
        public static new async Task<TransformStreamInProcess> CreateAsync(IJSRuntime jSRuntime, Transformer? transformer = null, ByteLengthQueuingStrategy? writableStrategy = null, CountQueuingStrategy? readableStrategy = null)
        {
            return await CreatePrivateAsync(jSRuntime, transformer, writableStrategy?.JSReference, readableStrategy?.JSReference);
        }

        /// <summary>
        /// Constructs a wrapper instance using the standard constructor.
        /// </summary>
        /// <param name="jSRuntime">An IJSRuntime instance.</param>
        /// <param name="transformer">An <see cref="Transformer"/> that which implements the Start, Transform, and/or Cancel methods.</param>
        /// <param name="writableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <param name="readableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <returns>A wrapper instance for a <see cref="TransformStream"/>.</returns>
        public static new async Task<TransformStreamInProcess> CreateAsync(IJSRuntime jSRuntime, Transformer? transformer = null, CountQueuingStrategy? writableStrategy = null, QueuingStrategy? readableStrategy = null)
        {
            return await CreatePrivateAsync(jSRuntime, transformer, writableStrategy?.JSReference, readableStrategy);
        }

        /// <summary>
        /// Constructs a wrapper instance using the standard constructor.
        /// </summary>
        /// <param name="jSRuntime">An IJSRuntime instance.</param>
        /// <param name="transformer">An <see cref="Transformer"/> that which implements the Start, Transform, and/or Cancel methods.</param>
        /// <param name="writableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <param name="readableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <returns>A wrapper instance for a <see cref="TransformStream"/>.</returns>
        public static new async Task<TransformStreamInProcess> CreateAsync(IJSRuntime jSRuntime, Transformer? transformer = null, CountQueuingStrategy? writableStrategy = null, ByteLengthQueuingStrategy? readableStrategy = null)
        {
            return await CreatePrivateAsync(jSRuntime, transformer, writableStrategy?.JSReference, readableStrategy?.JSReference);
        }

        /// <summary>
        /// Constructs a wrapper instance using the standard constructor.
        /// </summary>
        /// <param name="jSRuntime">An IJSRuntime instance.</param>
        /// <param name="transformer">An <see cref="Transformer"/> that which implements the Start, Transform, and/or Cancel methods.</param>
        /// <param name="writableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <param name="readableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
        /// <returns>A wrapper instance for a <see cref="TransformStream"/>.</returns>
        public static new async Task<TransformStreamInProcess> CreateAsync(IJSRuntime jSRuntime, Transformer? transformer = null, CountQueuingStrategy? writableStrategy = null, CountQueuingStrategy? readableStrategy = null)
        {
            return await CreatePrivateAsync(jSRuntime, transformer, writableStrategy?.JSReference, readableStrategy?.JSReference);
        }

        private static async Task<TransformStreamInProcess> CreatePrivateAsync(IJSRuntime jSRuntime, object? underlyingSource = null, object? writableStrategy = null, object? readableStrategy = null)
        {
            IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
            IJSInProcessObjectReference jSInstance = await inProcesshelper.InvokeAsync<IJSInProcessObjectReference>("constructReadableStream", underlyingSource, writableStrategy, readableStrategy);
            return new TransformStreamInProcess(jSRuntime, inProcesshelper, jSInstance, new() { DisposesJSReference = true });
        }

        /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSInProcessObjectReference, CreationOptions)"/>
        protected TransformStreamInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
        {
            this.inProcessHelper = inProcessHelper;
            JSReference = jSReference;
        }

        public ReadableStreamInProcess Readable
        {
            get
            {
                IJSInProcessObjectReference jSInstance = inProcessHelper.Invoke<IJSInProcessObjectReference>("getAttribute", JSReference, "readable");
                return new ReadableStreamInProcess(JSRuntime, inProcessHelper, jSInstance, new() { DisposesJSReference = true });
            }
            set => inProcessHelper.InvokeVoid("setAttribute", JSReference, "readable", value.JSReference);
        }

        public WritableStreamInProcess Writable
        {
            get
            {
                IJSInProcessObjectReference jSInstance = inProcessHelper.Invoke<IJSInProcessObjectReference>("getAttribute", JSReference, "writable");
                return new WritableStreamInProcess(JSRuntime, inProcessHelper, jSInstance, new() { DisposesJSReference = true });
            }
            set => inProcessHelper.InvokeVoid("setAttribute", JSReference, "writable", value.JSReference);
        }
    }
}
