using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Streams;

/// <summary>
/// <see href="https://webidl.spec.whatwg.org/#ArrayBufferView">WebIDL browser specs</see>
/// </summary>
public class ArrayBufferView
{
    public readonly IJSObjectReference JSReference;

    public static async Task<ArrayBufferView> CreateByteArrayAsync(IJSRuntime jSRuntime, int size)
    {
        IJSObjectReference helper = await jSRuntime.GetHelperAsync();
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("constructByteArray", size);
        return new ArrayBufferView(jSInstance);
    }

    internal ArrayBufferView(IJSObjectReference jSReference)
    {
        JSReference = jSReference;
    }
}
