using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams
{
    /// <summary>
    /// <see href="https://streams.spec.whatwg.org/#writablestreamdefaultwriter">Streams browser specs</see>
    /// </summary>
    public class WritableStreamDefaultWriter : BaseJSWrapper, IJSCreatable<WritableStreamDefaultWriter>
    {
        /// <summary>
        /// Constructs a wrapper instance for a given JS Instance of a <see cref="WritableStreamDefaultWriter"/>.
        /// </summary>
        /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
        /// <param name="jSReference">A JS reference to an existing <see cref="WritableStreamDefaultWriter"/>.</param>
        /// <returns>A wrapper instance for a <see cref="WritableStreamDefaultWriter"/>.</returns>
        [Obsolete("This will be removed in the next major release as all creator methods should be asynchronous for uniformity. Use CreateAsync instead.")]
        public static WritableStreamDefaultWriter Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
        {
            return new WritableStreamDefaultWriter(jSRuntime, jSReference, new());
        }

        /// <inheritdoc/>
        public static async Task<WritableStreamDefaultWriter> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
        {
            return await CreateAsync(jSRuntime, jSReference, new());
        }

        /// <inheritdoc/>
        public static Task<WritableStreamDefaultWriter> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
        {
            return Task.FromResult(new WritableStreamDefaultWriter(jSRuntime, jSReference, options));
        }

        /// <summary>
        /// Constructs a <see cref="WritableStreamDefaultWriter"/> from some <see cref="WritableStream"/>.
        /// </summary>
        /// <param name="jSRuntime">An IJSRuntime instance.</param>
        /// <param name="stream">A <see cref="WritableStream"/> wrapper instance.</param>
        /// <returns></returns>
        public static async Task<WritableStreamDefaultWriter> CreateAsync(IJSRuntime jSRuntime, WritableStream stream)
        {
            IJSObjectReference helper = await jSRuntime.GetHelperAsync();
            IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructWritableStreamDefaultReader", stream.JSReference);
            return new WritableStreamDefaultWriter(jSRuntime, jSInstance, new() { DisposesJSReference = true });
        }

        /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
        protected WritableStreamDefaultWriter(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options) { }

        /// <summary>
        /// A JS reference to the promise related to closing the writer.
        /// </summary>
        /// <returns></returns>
        public async Task<IJSObjectReference> GetClosedAsync()
        {
            IJSObjectReference helper = await helperTask.Value;
            return await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "closed");
        }

        /// <summary>
        /// The size desired to fill the writers internal queue. It can be negative, if the queue is over-full.
        /// </summary>
        /// <returns>It will be null if the stream cannot be successfully written to (due to either being errored, or having an abort queued up). It will return zero if the stream is closed.</returns>
        public async Task<double?> GetDesiredSizeAsync()
        {
            IJSObjectReference helper = await helperTask.Value;
            return await helper.InvokeAsync<double?>("getAttribute", JSReference, "desiredSize");
        }

        /// <summary>
        /// A JS reference to a promise that will be fulfilled when the <see cref="GetDesiredSizeAsync"/> changes to a positive number meaning the internal queue of the writer is ready to receive data.
        /// </summary>
        /// <returns></returns>
        public async Task<IJSObjectReference> GetReadyAsync()
        {
            IJSObjectReference helper = await helperTask.Value;
            return await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "ready");
        }

        /// <summary>
        /// Aborts the stream. The same as <see cref="WritableStream.AbortAsync"/>.
        /// </summary>
        /// <returns></returns>
        public async Task AbortAsync()
        {
            await JSReference.InvokeVoidAsync("abort");
        }

        /// <summary>
        /// Closes the stream. The same as <see cref="WritableStream.CloseAsync"/>.
        /// </summary>
        /// <returns></returns>
        public async Task CloseAsync()
        {
            await JSReference.InvokeVoidAsync("close");
        }

        /// <summary>
        /// Releases the writer's lock.
        /// </summary>
        /// <returns></returns>
        public async Task ReleaseLockAsync()
        {
            await JSReference.InvokeVoidAsync("releaseLock");
        }

        /// <summary>
        /// Writes the chunk to the writable stream once any previous writes have finished successfully.
        /// </summary>
        /// <param name="chunk"></param>
        /// <returns></returns>
        public async Task WriteAsync(IJSObjectReference chunk)
        {
            await JSReference.InvokeVoidAsync("write", chunk);
        }
    }
}
