using BlazorServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace IntegrationTests.Infrastructure;

public class JSRuntimeEvaluationContext(IJSRuntime jSRuntime) : EvaluationContext
{
    public IJSRuntime JSRuntime => jSRuntime;

    public static JSRuntimeEvaluationContext Create(IServiceProvider provider)
    {
        IJSRuntime jSRuntime = provider.GetRequiredService<IJSRuntime>();

        return new JSRuntimeEvaluationContext(jSRuntime);
    }
}
