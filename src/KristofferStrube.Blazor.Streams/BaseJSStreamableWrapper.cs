using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams
{
    /// <summary>
    /// A base class for all streamable wrappers in Blazor.Streams.
    /// </summary>
    public abstract class BaseJSStreamableWrapper : Stream, IAsyncDisposable, IJSWrapper
    {
        /// <summary>
        /// A lazily loaded task that evaluates to a helper module instance from the Blazor.Streams library.
        /// </summary>
        protected readonly Lazy<Task<IJSObjectReference>> helperTask;

        /// <inheritdoc/>
        public IJSRuntime JSRuntime { get; }

        /// <inheritdoc/>
        public IJSObjectReference JSReference { get; }

        /// <inheritdoc/>
        public bool DisposesJSReference { get; }

        /// <inheritdoc cref="IJSCreatable{T}.CreateAsync(IJSRuntime, IJSObjectReference, CreationOptions)"/>
        protected internal BaseJSStreamableWrapper(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
        {
            helperTask = new(() => jSRuntime.GetHelperAsync());
            JSRuntime = jSRuntime;
            JSReference = jSReference;
            DisposesJSReference = options.DisposesJSReference;
        }

        public override async ValueTask DisposeAsync()
        {
            await base.DisposeAsync();
            if (helperTask.IsValueCreated)
            {
                IJSObjectReference module = await helperTask.Value;
                await module.DisposeAsync();
            }
            GC.SuppressFinalize(this);
        }
    }
}
