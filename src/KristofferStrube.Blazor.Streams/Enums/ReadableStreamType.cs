using System.ComponentModel;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Streams
{
    /// <summary>
    /// <see href="https://streams.spec.whatwg.org/#enumdef-readablestreamtype">Streams browser specs</see>
    /// </summary>
    [JsonConverter(typeof(EnumDescriptionConverter<ReadableStreamType>))]
    public enum ReadableStreamType
    {
        [Description("bytes")]
        Bytes,
    }
}
