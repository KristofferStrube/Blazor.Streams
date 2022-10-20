using Microsoft.JSInterop;
using System.Reflection.PortableExecutable;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#readablestreamdefaultreader">Streams browser specs</see>
/// </summary>
public class ReadableStreamDefaultReader : ReadableStreamReader, IAsyncEnumerable<IJSObjectReference>
{
    /// <summary>
    /// Constructs a <see cref="ReadableStreamDefaultReader"/> from some <see cref="ReadableStream"/>.
    /// </summary>
    /// <param name="jSRuntime">An IJSRuntime instance.</param>
    /// <param name="stream">A <see cref="ReadableStream"/> wrapper instance.</param>
    /// <returns></returns>
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
    public ReadableStreamDefaultReader(IJSRuntime jSRuntime, IJSObjectReference jSReference) : base(jSRuntime, jSReference) { }

    /// <summary>
    /// Reads a chunk of a stream.
    /// </summary>
    /// <returns>The next chunk of the underlying <see cref="ReadableStream"/>.</returns>
    public async Task<ReadableStreamReadResult> ReadAsync()
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("read");
        return new ReadableStreamReadResult(jSRuntime, jSInstance);
    }

    public async IAsyncEnumerator<IJSObjectReference> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        var read = await ReadAsync();
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
    public async IAsyncEnumerable<byte[]> IterateByteArrays(CancellationToken cancellationToken = default)
    {
        IJSObjectReference helper = await helperTask.Value;
        var read = await ReadAsync();
        while (!await read.GetDoneAsync() && !cancellationToken.IsCancellationRequested)
        {
            var value = await read.GetValueAsync();
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
    public async IAsyncEnumerable<string> IterateStrings(System.Text.Encoding? encoding = null, CancellationToken cancellationToken = default)
    {
        if (encoding is null)
        {
            encoding = System.Text.Encoding.ASCII;
        }
        IJSObjectReference helper = await helperTask.Value;
        var read = await ReadAsync();
        while (!await read.GetDoneAsync() && !cancellationToken.IsCancellationRequested)
        {
            var value = await read.GetValueAsync();
            var byteArray = await helper.InvokeAsync<byte[]>("byteArray", cancellationToken, value);
            yield return encoding.GetString(byteArray);
            read = await ReadAsync();
        }
    }
}