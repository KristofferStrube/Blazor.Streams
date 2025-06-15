using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams
{
    /// <summary>
    /// <see href="https://streams.spec.whatwg.org/#typedefdef-readablestreamcontroller">Streams browser specs</see>
    /// </summary>
    public abstract class ReadableStreamController : BaseJSWrapper
    {
        /// <inheritdoc cref="IJSCreatable{T}.CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
        protected ReadableStreamController(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }

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
        /// Closes the controlled stream once all previously enqueued chunks have been read.
        /// </summary>
        /// <returns></returns>
        public async Task CloseAsync()
        {
            await JSReference.InvokeVoidAsync("close");
        }

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
