using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-queuingstrategy">Streams browser specs</see>
/// </summary>
public class QueingStrategy
{
    [JsonPropertyName("highWaterMark")]
    public double HighWaterMark { get; set; }

    [JsonPropertyName("size")]
    public double Size { get; set; }
}
