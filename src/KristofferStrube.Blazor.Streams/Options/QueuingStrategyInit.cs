using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Streams
{
    /// <summary>
    /// <see href="https://streams.spec.whatwg.org/#dictdef-queuingstrategyinit">Streams browser specs</see>
    /// </summary>
    public class QueuingStrategyInit
    {
        public QueuingStrategyInit(double highWaterMark)
        {
            HighWaterMark = highWaterMark;
        }

        [JsonPropertyName("highWaterMark")]
        public double HighWaterMark { get; set; }
    }
}
