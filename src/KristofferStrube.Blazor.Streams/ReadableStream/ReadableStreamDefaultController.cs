using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams
{
    /// <summary>
    /// <see href="https://streams.spec.whatwg.org/#readablestreamdefaultcontroller">Streams browser specs</see>
    /// </summary>
    public class ReadableStreamDefaultController : ReadableStreamController, IJSCreatable<ReadableStreamDefaultController>
    {
        /// <inheritdoc/>
        public static async Task<ReadableStreamDefaultController> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
        {
            return await CreateAsync(jSRuntime, jSReference, new());
        }

        /// <inheritdoc/>
        public static Task<ReadableStreamDefaultController> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
        {
            return Task.FromResult(new ReadableStreamDefaultController(jSRuntime, jSReference, options));
        }

        /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
        protected internal ReadableStreamDefaultController(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }

        /// <summary>
        /// Enqueues the chunk in the controlled stream.
        /// </summary>
        /// <param name="chunk">A JS reference to a chunk.</param>
        /// <returns></returns>
        public async Task EnqueueAsync(IJSObjectReference chunk)
        {
            await JSReference.InvokeVoidAsync("enqueue", chunk);
        }
    }
}
