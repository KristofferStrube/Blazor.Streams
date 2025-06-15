using System.ComponentModel;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Streams
{
    /// <summary>
    /// <see href="https://streams.spec.whatwg.org/#enumdef-readablestreamreadermode">Streams browser specs</see>
    /// </summary>
    [JsonConverter(typeof(EnumDescriptionConverter<ReadableStreamReaderMode>))]
    public enum ReadableStreamReaderMode
    {
        [Description("byob")]
        Byob,
    }
}
