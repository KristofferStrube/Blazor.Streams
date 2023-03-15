namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#generictransformstream">Streams browser specs</see>
/// </summary>
public interface IGenericTransformStreamInProcess : IGenericTransformStream
{
    public new Task<ReadableStreamInProcess> GetReadableAsync();
    public new Task<WritableStreamInProcess> GetWritableAsync();
}
