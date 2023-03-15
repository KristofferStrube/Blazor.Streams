using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#ws-default-controller-class-definition">Streams browser specs</see>
/// </summary>
public class TransformStreamDefaultController : BaseJSWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="TransformStreamDefaultController"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="TransformStreamDefaultController"/>.</param>
    /// <returns>A wrapper instance for a <see cref="TransformStreamDefaultController"/>.</returns>
    [Obsolete("This will be removed in the next major release as all creator methods should be asynchronous for uniformity. Use CreateAsync instead.")]
    public static TransformStreamDefaultController Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return new TransformStreamDefaultController(jSRuntime, jSReference);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="TransformStreamDefaultController"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="TransformStreamDefaultController"/>.</param>
    /// <returns>A wrapper instance for a <see cref="TransformStreamDefaultController"/>.</returns>
    public static Task<TransformStreamDefaultController> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new TransformStreamDefaultController(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="TransformStreamDefaultController"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="TransformStreamDefaultController"/>.</param>
    internal TransformStreamDefaultController(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// The desired size to fill the controlled stream's internal queue.
    /// </summary>
    /// <returns>Negative values means that the queue is overfull.</returns>
    public async Task<double?> GetDesiredSizeAsync()
    {
        IJSObjectReference helper = await helperTask.Value;
        return await helper.InvokeAsync<double?>("getAttribute", JSReference, "desiredSize");
    }

    /// <summary>
    /// Enqueues a chunk on the readable side of the transfor stream.
    /// </summary>
    /// <param name="chunk">A JS reference to a chunk.</param>
    /// <returns></returns>
    public async Task EnqueueAsync(IJSObjectReference chunk)
    {
        await JSReference.InvokeVoidAsync("enqueue", chunk);
    }

    /// <summary>
    /// Errors both the readable and writable side of the transform stream.
    /// </summary>
    /// <returns></returns>
    public async Task ErrorAsync()
    {
        await JSReference.InvokeVoidAsync("error");
    }

    /// <summary>
    /// Closes the readable side and errors the writable side.
    /// </summary>
    /// <returns></returns>
    public async Task TerminateAsync()
    {
        await JSReference.InvokeVoidAsync("terminate");
    }
}
