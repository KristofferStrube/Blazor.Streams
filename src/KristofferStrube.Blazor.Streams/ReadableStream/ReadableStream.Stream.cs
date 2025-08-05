using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

public partial class ReadableStream
{
    private ReadableStreamDefaultReader? reader;

    public override bool CanRead => true;

    public override bool CanSeek => false;

    public override bool CanWrite => false;

    /// <summary>
    /// We can't check the length of a <see cref="ReadableStream"/>.
    /// </summary>
    public override long Length => 0;

    public override long Position { get; set; }

    public override void Flush()
    {
        throw new InvalidOperationException($"Flushing a {nameof(ReadableStream)} is not supported as its underlying data source is a stream.");
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        throw new InvalidOperationException($"You can't invoke synchronous Stream methods on {nameof(ReadableStream)} because the underlying JS method is asynchronous.");
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        throw new InvalidOperationException($"Seeking in a {nameof(ReadableStream)} is not supported as its underlying data source is a stream.");
    }

    public override void SetLength(long value)
    {
        throw new InvalidOperationException($"Changing the length of {nameof(ReadableStream)} is not supported as it is meant for reading.");
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        throw new InvalidOperationException($"Writing to {nameof(ReadableStream)} is not supported as it is meant for reading.");
    }

    private byte[]? additionalDataRead;

    public override async ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default)
    {
        // If there is more than the desired buffer size available from previous reads then use those bytes.
        if (additionalDataRead?.Length > buffer.Length)
        {
            additionalDataRead[..buffer.Length].CopyTo(buffer);
            additionalDataRead = additionalDataRead[buffer.Length..];
            return buffer.Length;
        }

        // If there is exactly enough data from previous reads, then use those bytes and clear the old data.
        if (additionalDataRead?.Length == buffer.Length)
        {
            additionalDataRead.CopyTo(buffer);
            additionalDataRead = null;
            return buffer.Length;
        }

        // There is some data left from previous reads but not enough to fill the buffer.
        int bytesCopiedFromExcessDataReadPreviously = 0;
        if (additionalDataRead is not null)
        {
            additionalDataRead.CopyTo(buffer);
            bytesCopiedFromExcessDataReadPreviously = additionalDataRead.Length;
            additionalDataRead = null;
        }

        reader ??= await GetDefaultReaderAsync();
        ReadableStreamReadResult read = await reader.ReadAsync();
        if (!await read.GetDoneAsync())
        {
            IJSObjectReference jSValue = await read.GetValueAsync();
            await using Uint8Array value = await Uint8Array.CreateAsync(JSRuntime, jSValue);
            byte[] data = await value.GetAsArrayAsync();
            int bytesNeededToFillBuffer = buffer.Length - bytesCopiedFromExcessDataReadPreviously;
            if (data.Length > bytesNeededToFillBuffer)
            {
                data[..bytesNeededToFillBuffer].CopyTo(buffer[bytesCopiedFromExcessDataReadPreviously..]);
                additionalDataRead = data[bytesNeededToFillBuffer..];
            }
            else
            {
                data.CopyTo(buffer[bytesCopiedFromExcessDataReadPreviously..]);
            }
            return Math.Min(data.Length, buffer.Length);
        }
        await reader.ReleaseLockAsync();
        reader = null;
        return 0;
    }
}
