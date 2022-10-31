using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

public partial class WritableStream
{
    private WritableStreamDefaultWriter? writer;

    public override bool CanRead => false;

    public override bool CanSeek => false;

    public override bool CanWrite => true;

    /// <summary>
    /// We can't check the length of a <see cref="ReadableStream"/>.
    /// </summary>
    public override long Length => 0;

    public override long Position { get; set; }

    public override void Flush()
    {
        throw new InvalidOperationException($"You can't invoke synchronous Stream methods on {nameof(WritableStream)} because the underlying JS method is asynchronous.");
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        throw new InvalidOperationException($"Reading a {nameof(WritableStream)} is not supported as it is meant for writing.");
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new InvalidOperationException($"Seeking in a {nameof(WritableStream)} is not supported as its underlying data source is a stream.");
    }

    public override void SetLength(long value)
    {
        throw new InvalidOperationException($"Changing the length of {nameof(WritableStream)} is not supported as its underlying data source is a stream.");
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new InvalidOperationException($"You can't invoke synchronous Stream methods on {nameof(WritableStream)} because the underlying JS method is asynchronous.");
    }

    public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
    {
        await WriteAsync(buffer, cancellationToken);
    }

    public override async ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default)
    {
        writer ??= await GetWriterAsync();
        var helper = await helperTask.Value;
        await writer.WriteAsync(await helper.InvokeAsync<IJSObjectReference>("valueOf", buffer.ToArray()));
    }

    public override async Task FlushAsync(CancellationToken cancellationToken)
    {
        if (writer is null) return;
        await writer.CloseAsync();
        writer = null;
    }
}
