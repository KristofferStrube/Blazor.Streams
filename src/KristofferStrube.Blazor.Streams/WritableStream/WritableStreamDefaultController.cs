using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#ws-default-controller-class-definition">Streams browser specs</see>
/// </summary>
public class WritableStreamDefaultController : BaseJSWrapper
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="WritableStreamDefaultController"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="WritableStreamDefaultController"/>.</param>
    /// <returns>A wrapper instance for a <see cref="WritableStreamDefaultController"/>.</returns>
    public static WritableStreamDefaultController Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return new WritableStreamDefaultController(jSRuntime, jSReference);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="WritableStreamDefaultController"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="WritableStreamDefaultController"/>.</param>
    internal WritableStreamDefaultController(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    public async Task ErrorAsync()
    {
        await JSReference.InvokeVoidAsync("error");
    }
}
