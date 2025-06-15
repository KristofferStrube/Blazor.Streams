using Microsoft.JSInterop;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Streams
{
    /// <summary>
    /// <see href="https://streams.spec.whatwg.org/#dictdef-queuingstrategy">Streams browser specs</see>
    /// </summary>
    public class QueuingStrategy
    {
        public QueuingStrategy()
        {
            ObjRef = DotNetObjectReference.Create(this);
        }

        public DotNetObjectReference<QueuingStrategy> ObjRef { get; set; }

        [JsonPropertyName("highWaterMark")]
        public double HighWaterMark { get; set; }

        [JsonIgnore]
        public Func<IJSObjectReference, double>? Size { get; set; }

        [JSInvokable]
        public double InvokeSize(IJSObjectReference chunk)
        {
            return Size is null ? 0 : Size.Invoke(chunk);
        }
    }
}
