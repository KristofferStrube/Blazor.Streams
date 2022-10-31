using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

public partial class ReadableStream : Stream
{
    private ReadableStreamDefaultReader? reader;

    public override bool CanRead => true;

    public override bool CanSeek => false;

    public override bool CanWrite => false;

    /// <summary>
    /// We can't check the length of the file synchronously.
    /// </summary>
    public override long Length => 0;

    public override long Position { get; set; }

    public override void Flush()
    {
        throw new InvalidOperationException($"You can't invoke synchronous Stream methods on {nameof(ReadableStream)}. Use {nameof(ReadableStreamInProcess)} instead or use the async variant of this method instead.");
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        throw new InvalidOperationException($"You can't invoke synchronous Stream methods on {nameof(ReadableStream)}. Use {nameof(ReadableStreamInProcess)} instead or use the async variant of this method instead.");
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new InvalidOperationException($"Seeking in a {nameof(ReadableStream)} is not supported as it is a stream.");
    }

    public override void SetLength(long value)
    {
        throw new InvalidOperationException($"Writing to {nameof(ReadableStream)} is not supported as it is meant for reading.");
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new InvalidOperationException($"Writing to {nameof(ReadableStream)} is not supported as it is meant for reading.");
    }

    public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
    {
        if (reader is null)
        {
            reader = await GetDefaultReaderAsync();
        }
        var read = await reader.ReadAsync();
        if (!await read.GetDoneAsync())
        {
            var jSValue = await read.GetValueAsync();
            var helper = await helperTask.Value;
            var length = await helper.InvokeAsync<int>("getAttribute", jSValue, "length");
            (await helper.InvokeAsync<byte[]>("byteArray", jSValue)).CopyTo(buffer);
            return length;
        }
        await reader.ReleaseLockAsync();
        reader = null;
        return 0;
    }
}
