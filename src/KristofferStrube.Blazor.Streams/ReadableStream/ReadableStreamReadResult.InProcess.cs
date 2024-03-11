using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-readablestreamreadresult">Streams browser specs</see>
/// </summary>
public class ReadableStreamReadResultInProcess : ReadableStreamReadResult
{
    /// <summary>
    /// An in-process helper module instance from the Blazor.Streams library.
    /// </summary>
    protected readonly IJSInProcessObjectReference inProcessHelper;

    /// <inheritdoc/>
    public static async Task<ReadableStreamReadResultInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static async Task<ReadableStreamReadResultInProcess> CreateAsync(IJSRuntime jSRuntime, IJSInProcessObjectReference jSReference, CreationOptions options)
    {
        IJSInProcessObjectReference inProcesshelper = await jSRuntime.GetInProcessHelperAsync();
        return new ReadableStreamReadResultInProcess(jSRuntime, inProcesshelper, jSReference, options);
    }

    /// <inheritdoc cref="CreateAsync(IJSRuntime, IJSInProcessObjectReference, CreationOptions)"/>
    internal ReadableStreamReadResultInProcess(IJSRuntime jSRuntime, IJSInProcessObjectReference inProcessHelper, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
        this.inProcessHelper = inProcessHelper;
    }

    /// <summary>
    /// A JS Reference to a chunk of data.
    /// </summary>
    /// <returns>An <see cref="IJSInProcessObjectReference"/> to a value which can be of <c>any</c> type.</returns>
    public IJSInProcessObjectReference Value => inProcessHelper.Invoke<IJSInProcessObjectReference>("getAttribute", JSReference, "value");

    /// <summary>
    /// Indicates whether this is the last read which means that <see cref="Value"/> will be <c>undefined</c>.
    /// </summary>
    /// <returns><see langword="true"/> if the chunk is the last which contains no <see cref="Value"/> else <see langword="false"/></returns>
    public bool Done => inProcessHelper.Invoke<bool>("getAttribute", JSReference, "done");
}
