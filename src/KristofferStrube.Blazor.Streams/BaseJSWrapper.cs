﻿using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

public abstract class BaseJSWrapper : IAsyncDisposable, IJSWrapper
{
    public IJSObjectReference JSReference { get; }
    public IJSRuntime JSRuntime { get; }

    protected readonly Lazy<Task<IJSObjectReference>> helperTask;

    /// <summary>
    /// Constructs a wrapper instance for an equivalent JS instance.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing JS instance that should be wrapped..</param>
    internal BaseJSWrapper(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        helperTask = new(() => jSRuntime.GetHelperAsync());
        JSReference = jSReference;
        this.JSRuntime = jSRuntime;
    }

    public async ValueTask DisposeAsync()
    {
        if (helperTask.IsValueCreated)
        {
            IJSObjectReference module = await helperTask.Value;
            await module.DisposeAsync();
        }
        GC.SuppressFinalize(this);
    }
}
