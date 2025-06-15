using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams
{
    /// <summary>
    /// <see href="https://webidl.spec.whatwg.org/#ArrayBufferView">WebIDL browser specs</see>
    /// </summary>
    [Obsolete("A richer implementation has been introduced in Blazor.WebIDL so we shouldn't use this simple wrapper anymore. Use IArrayBufferView from Blazor.WebIDL instead.")]
    public class ArrayBufferView
    {
        public readonly IJSObjectReference JSReference;

        [Obsolete("A richer implementation has been introduced in Blazor.WebIDL so we shouldn't use this simple wrapper anymore. Use IArrayBufferView from Blazor.WebIDL instead.")]
        public static async Task<ArrayBufferView> CreateByteArrayAsync(IJSRuntime jSRuntime, int size)
        {
            IJSObjectReference helper = await jSRuntime.GetHelperAsync();
            IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructByteArray", size);
            return new ArrayBufferView(jSInstance);
        }

        [Obsolete("A richer implementation has been introduced in Blazor.WebIDL so we shouldn't use this simple wrapper anymore. Use IArrayBufferView from Blazor.WebIDL instead.")]
        protected internal ArrayBufferView(IJSObjectReference jSReference)
        {
            JSReference = jSReference;
        }
    }
}
