using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

public class TransformStreamInProcess : TransformStream
{
    public new IJSInProcessObjectReference JSReference;
    private readonly IJSInProcessObjectReference inProcessHelper;

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="TransformStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="TransformStream"/>.</param>
    /// <returns>A wrapper instance for a <see cref="TransformStream"/>.</returns>
    public static async Task<TransformStreamInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
        return new TransformStreamInProcess(jSRuntime, inProcesshelper, jSReference);
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="transformer">An <see cref="Transformer"/> that which implements the Start, Pull, and/or Cancel methods.</param>
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
    /// <param name="transformer">An <see cref="Transformer"/> that which implements the Start, Pull, and/or Cancel methods.</param>
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
    /// <param name="transformer">An <see cref="Transformer"/> that which implements the Start, Pull, and/or Cancel methods.</param>
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
    /// <param name="transformer">An <see cref="Transformer"/> that which implements the Start, Pull, and/or Cancel methods.</param>
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
    /// <param name="transformer">An <see cref="Transformer"/> that which implements the Start, Pull, and/or Cancel methods.</param>
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
    /// <param name="transformer">An <see cref="Transformer"/> that which implements the Start, Pull, and/or Cancel methods.</param>
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
    /// <param name="transformer">An <see cref="Transformer"/> that which implements the Start, Pull, and/or Cancel methods.</param>
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
    /// <param name="transformer">An <see cref="Transformer"/> that which implements the Start, Pull, and/or Cancel methods.</param>
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
    /// <param name="transformer">An <see cref="Transformer"/> that which implements the Start, Pull, and/or Cancel methods.</param>
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
        return new TransformStreamInProcess(jSRuntime, inProcesshelper, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="TransformStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="inProcessHelper">An in process helper instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="TransformStreamInProcess"/>.</param>
    internal TransformStreamInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSInProcessObjectReference jSReference) : base(jSRuntime, jSReference)
    {
        this.inProcessHelper = inProcessHelper;
        JSReference = jSReference;
    }

    public ReadableStreamInProcess Readable
    {
        get
        {
            IJSInProcessObjectReference jSInstance = inProcessHelper.Invoke<IJSInProcessObjectReference>("getAttribute", JSReference, "readable");
            return new ReadableStreamInProcess(jSRuntime, inProcessHelper, jSInstance);
        }
        set => inProcessHelper.InvokeVoid("setAttribute", JSReference, "readable", value.JSReference);
    }

    public WritableStreamInProcess Writable
    {
        get
        {
            IJSInProcessObjectReference jSInstance = inProcessHelper.Invoke<IJSInProcessObjectReference>("getAttribute", JSReference, "writable");
            return new WritableStreamInProcess(jSRuntime, inProcessHelper, jSInstance);
        }
        set => inProcessHelper.InvokeVoid("setAttribute", JSReference, "writable", value.JSReference);
    }
}
