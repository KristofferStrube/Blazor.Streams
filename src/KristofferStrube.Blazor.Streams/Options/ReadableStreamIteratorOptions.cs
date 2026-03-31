using KristofferStrube.Blazor.WebIDL;
using System.Text.Json.Serialization;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// Options for how the values of a <see cref="ReadableStream"/> should be iterated when calling <c>ValuesAsync</c> on a <see cref="IValueAsyncIterable{TAsyncIterable, TValue, TIteratorOptions}"/>.
/// </summary>
/// <remarks><see href="https://streams.spec.whatwg.org/#dictdef-readablestreamiteratoroptions">See the API definition here</see>.</remarks>
public class ReadableStreamIteratorOptions
{
    /// <summary>
    /// Asynchronously iterating over the stream will lock it, preventing any other consumer from acquiring a reader.
    /// The lock will be released if the <see cref="AsyncIterator{TElement}.ReturnAsync"/> method is called, e.g. by breaking out of the loop.
    /// By default, calling <see cref="AsyncIterator{TElement}.ReturnAsync"/> will also cancel the stream.
    /// To prevent this set this to <see langword="true"/>.
    /// </summary>
    [JsonPropertyName("preventCancel")]
    public bool PreventCancel { get; set; }
}
