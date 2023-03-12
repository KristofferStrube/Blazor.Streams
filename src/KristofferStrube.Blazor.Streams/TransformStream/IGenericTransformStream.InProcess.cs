namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#generictransformstream">Streams browser specs</see>
/// </summary>
public interface IGenericTransformStreamInProcess : IGenericTransformStream
{
    public ReadableStreamInProcess Readable { get; }
    public WritableStreamInProcess Writable { get; }
}
