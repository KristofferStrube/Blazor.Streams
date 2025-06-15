using KristofferStrube.Blazor.DOM;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Streams
{
    /// <summary>
    /// <see href="https://streams.spec.whatwg.org/#dictdef-streampipeoptions">Streams browser specs</see>
    /// </summary>
    public class StreamPipeOptions
    {
        /// <summary>
        /// Decides whether the destination will be closed when the <see cref="ReadableStream"/> is closed.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("preventClose")]
        public bool PreventClose { get; set; }

        /// <summary>
        /// Decides whether the destination will be aborted when the <see cref="ReadableStream"/> is aborted.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("preventAbort")]
        public bool PreventAbort { get; set; }

        /// <summary>
        /// Decides whether the <see cref="ReadableStream"/> will be aborted when the destination is aborted.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("preventCancel")]
        public bool PreventCancel { get; set; }

        /// <summary>
        /// The signal option can be set to an <see cref="AbortSignal"/> to allow aborting an ongoing pipe operation via the corresponding <see cref="AbortController"/>.
        /// In this case, this source readable stream will be canceled, and destination aborted, unless the respective options <see cref="PreventCancel"/> or <see cref="PreventAbort"/> are set.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [JsonPropertyName("signal")]
        public AbortSignal? Signal { get; set; }
    }
}
