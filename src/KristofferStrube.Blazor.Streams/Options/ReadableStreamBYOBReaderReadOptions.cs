using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-readablestreambyobreaderreadoptions">Streams browser specs</see>
/// </summary>
public class ReadableStreamBYOBReaderReadOptions
{
    /// <summary>
    /// The minimal number of elements needed to be read for the <see cref="ReadableStreamBYOBReader.ReadAsync(WebIDL.IArrayBufferView, ReadableStreamBYOBReaderReadOptions?)"/> to finish.
    /// </summary>
    [JsonPropertyName("min")]
    public ulong Min { get; set; }
}
