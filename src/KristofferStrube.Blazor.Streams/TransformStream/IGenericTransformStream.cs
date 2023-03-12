using KristofferStrube.Blazor.WebIDL;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#generictransformstream">Streams browser specs</see>
/// </summary>
public interface IGenericTransformStream : IJSWrapper
{
    public Task<ReadableStream> GetReadableAsync();
    public Task<WritableStream> GetWritableAsync();
}
