using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams
{
    /// <summary>
    /// <see href="https://streams.spec.whatwg.org/#ws-default-controller-class-definition">Streams browser specs</see>
    /// </summary>
    public class WritableStreamDefaultController : BaseJSWrapper, IJSCreatable<WritableStreamDefaultController>
    {
        /// <summary>
        /// Constructs a wrapper instance for a given JS Instance of a <see cref="WritableStreamDefaultController"/>.
        /// </summary>
        /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
        /// <param name="jSReference">A JS reference to an existing <see cref="WritableStreamDefaultController"/>.</param>
        /// <returns>A wrapper instance for a <see cref="WritableStreamDefaultController"/>.</returns>
        [Obsolete("This will be removed in the next major release as all creator methods should be asynchronous for uniformity. Use CreateAsync instead.")]
        public static WritableStreamDefaultController Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
        {
            return new WritableStreamDefaultController(jSRuntime, jSReference, new());
        }

        /// <inheritdoc/>
        public static async Task<WritableStreamDefaultController> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
        {
            return await CreateAsync(jSRuntime, jSReference, new());
        }

        /// <inheritdoc/>
        public static Task<WritableStreamDefaultController> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
        {
            return Task.FromResult(new WritableStreamDefaultController(jSRuntime, jSReference, options));
        }

        /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
        protected internal WritableStreamDefaultController(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }

        /// <summary>
        /// Errors the controlled stream so that all future interactions will fail.
        /// </summary>
        /// <returns></returns>
        public async Task ErrorAsync()
        {
            await JSReference.InvokeVoidAsync("error");
        }
    }
}
