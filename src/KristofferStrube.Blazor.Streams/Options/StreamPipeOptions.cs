using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://streams.spec.whatwg.org/#dictdef-streampipeoptions">Streams browser specs</see>
/// </summary>
public class StreamPipeOptions
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("preventClose")]
    public bool PreventClose { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("preventAbort")]
    public bool PreventAbort { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    [JsonPropertyName("preventCancel")]
    public bool PreventCancel { get; set; }

    // TODO: Add AbortSignal signal.
}
