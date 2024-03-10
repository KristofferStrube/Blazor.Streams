﻿using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#transformstream">Streams browser specs</see>
/// </summary>
public class TransformStream : BaseJSWrapper, IGenericTransformStream, IJSCreatable<TransformStream>
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="TransformStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="TransformStream"/>.</param>
    /// <returns>A wrapper instance for a <see cref="TransformStream"/>.</returns>
    [Obsolete("This will be removed in the next major release as all creator methods should be asynchronous for uniformity. Use CreateAsync instead.")]
    public static TransformStream Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return new TransformStream(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static async Task<TransformStream> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static Task<TransformStream> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new TransformStream(jSRuntime, jSReference, options));
    }

    /// <summary>
    /// Constructs a wrapper instance using the standard constructor.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="transformer">An <see cref="Transformer"/> that which implements the Start, Transform, and/or Cancel methods.</param>
    /// <param name="writableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
    /// <param name="readableStrategy">A queing strategy that specifies the chunk size and a high water mark.</param>
    /// <returns>A wrapper instance for a <see cref="TransformStream"/>.</returns>
    public static async Task<TransformStream> CreateAsync(IJSRuntime jSRuntime, Transformer? transformer = null, QueuingStrategy? writableStrategy = null, QueuingStrategy? readableStrategy = null)
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
    public static async Task<TransformStream> CreateAsync(IJSRuntime jSRuntime, Transformer? transformer = null, QueuingStrategy? writableStrategy = null, ByteLengthQueuingStrategy? readableStrategy = null)
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
    public static async Task<TransformStream> CreateAsync(IJSRuntime jSRuntime, Transformer? transformer = null, QueuingStrategy? writableStrategy = null, CountQueuingStrategy? readableStrategy = null)
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
    public static async Task<TransformStream> CreateAsync(IJSRuntime jSRuntime, Transformer? transformer = null, ByteLengthQueuingStrategy? writableStrategy = null, QueuingStrategy? readableStrategy = null)
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
    public static async Task<TransformStream> CreateAsync(IJSRuntime jSRuntime, Transformer? transformer = null, ByteLengthQueuingStrategy? writableStrategy = null, ByteLengthQueuingStrategy? readableStrategy = null)
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
    public static async Task<TransformStream> CreateAsync(IJSRuntime jSRuntime, Transformer? transformer = null, ByteLengthQueuingStrategy? writableStrategy = null, CountQueuingStrategy? readableStrategy = null)
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
    public static async Task<TransformStream> CreateAsync(IJSRuntime jSRuntime, Transformer? transformer = null, CountQueuingStrategy? writableStrategy = null, QueuingStrategy? readableStrategy = null)
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
    public static async Task<TransformStream> CreateAsync(IJSRuntime jSRuntime, Transformer? transformer = null, CountQueuingStrategy? writableStrategy = null, ByteLengthQueuingStrategy? readableStrategy = null)
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
    public static async Task<TransformStream> CreateAsync(IJSRuntime jSRuntime, Transformer? transformer = null, CountQueuingStrategy? writableStrategy = null, CountQueuingStrategy? readableStrategy = null)
    {
        return await CreatePrivateAsync(jSRuntime, transformer, writableStrategy?.JSReference, readableStrategy?.JSReference);
    }

    private static async Task<TransformStream> CreatePrivateAsync(IJSRuntime jSRuntime, object? underlyingSource = null, object? writableStrategy = null, object? readableStrategy = null)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructTransformStream", underlyingSource, writableStrategy, readableStrategy);
        return new TransformStream(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
    protected TransformStream(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }

    public async Task<ReadableStream> GetReadableAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "readable");
        return new ReadableStream(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    public async Task<WritableStream> GetWritableAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "writable");
        return await WritableStream.CreateAsync(JSRuntime, jSInstance);
    }
}
