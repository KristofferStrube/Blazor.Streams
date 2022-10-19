using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-readablestreamgetreaderoptions">Streams browser specs</see>
/// </summary>
public class ReadableStreamGetReaderOptions
{
    [JsonPropertyName("mode")]
    public ReadableStreamReaderMode Mode { get; set; }
}
