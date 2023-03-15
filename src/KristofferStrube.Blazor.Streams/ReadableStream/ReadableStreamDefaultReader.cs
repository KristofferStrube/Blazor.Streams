using Microsoft.JSInterop;
using System.Runtime.CompilerServices;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#readablestreamdefaultreader">Streams browser specs</see>
/// </summary>
public class ReadableStreamDefaultReader : ReadableStreamReader, IAsyncEnumerable<IJSObjectReference>
{
    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStreamDefaultReader"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamDefaultReader"/>.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableStreamDefaultReader"/>.</returns>
    [Obsolete("This will be removed in the next major release as all creator methods should be asynchronous for uniformity. Use CreateAsync instead.")]
    public static ReadableStreamDefaultReader Create(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return new ReadableStreamDefaultReader(jSRuntime, jSReference);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS Instance of a <see cref="ReadableStreamDefaultReader"/>.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamDefaultReader"/>.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableStreamDefaultReader"/>.</returns>
    public static Task<ReadableStreamDefaultReader> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return Task.FromResult(new ReadableStreamDefaultReader(jSRuntime, jSReference));
    }

    /// <summary>
    /// Constructs a <see cref="ReadableStreamDefaultReader"/> from some <see cref="ReadableStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="stream">A <see cref="ReadableStream"/> wrapper instance.</param>
    /// <returns>A wrapper instance for a <see cref="ReadableStreamDefaultReader"/>.</returns>
    public static async Task<ReadableStreamDefaultReader> CreateAsync(IJSRuntime jSRuntime, ReadableStream stream)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructReadableStreamDefaultReader", stream.JSReference);
        return new ReadableStreamDefaultReader(jSRuntime, jSInstance);
    }

    /// <summary>
    /// Constructs a wrapper instance for a given JS instance of a <see cref="ReadableStreamDefaultReader"/>.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="jSReference">A JS reference to an existing <see cref="ReadableStreamDefaultReader"/>.</param>
    internal ReadableStreamDefaultReader(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// Reads a chunk of a stream.
    /// </summary>
    /// <returns>The next chunk of the underlying <see cref="ReadableStream"/>.</returns>
    public async Task<ReadableStreamReadResult> ReadAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("read");
        return new ReadableStreamReadResult(JSRuntime, jSInstance);
    }

    public async IAsyncEnumerator<IJSObjectReference> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        ReadableStreamReadResult read = await ReadAsync();
        while (!await read.GetDoneAsync() && !cancellationToken.IsCancellationRequested)
        {
            yield return await read.GetValueAsync();
            read = await ReadAsync();
        }
    }

    /// <summary>
    /// Iterates the reader resulting in each chunk as byte arrays,
    /// </summary>
    /// <param name="cancellationToken">A cancellation token for breaking the enumeration.</param>
    /// <returns></returns>
    public async IAsyncEnumerable<byte[]> IterateByteArraysAsync([EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        IJSObjectReference helper = await helperTask.Value;
        ReadableStreamReadResult read = await ReadAsync();
        while (!await read.GetDoneAsync() && !cancellationToken.IsCancellationRequested)
        {
            IJSObjectReference value = await read.GetValueAsync();
            yield return await helper.InvokeAsync<byte[]>("byteArray", value);
            read = await ReadAsync();
        }
    }

    /// <summary>
    /// Iterates the reader resulting in each chunk as a string with some optional encoding. The defualt encodig is <see cref="System.Text.Encoding.ASCII"/>,
    /// </summary>
    /// <param name="cancellationToken">A cancellation token for breaking the enumeration.</param>
    /// <param name="encoding">A cancellation token for breaking the enumeration.</param>
    /// <returns></returns>
    public async IAsyncEnumerable<string> IterateStringsAsync(System.Text.Encoding? encoding = null, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        encoding ??= System.Text.Encoding.ASCII;
        IJSObjectReference helper = await helperTask.Value;
        ReadableStreamReadResult read = await ReadAsync();
        while (!await read.GetDoneAsync() && !cancellationToken.IsCancellationRequested)
        {
            IJSObjectReference value = await read.GetValueAsync();
            byte[] byteArray = await helper.InvokeAsync<byte[]>("byteArray", cancellationToken, value);
            yield return encoding.GetString(byteArray);
            read = await ReadAsync();
        }
    }
}