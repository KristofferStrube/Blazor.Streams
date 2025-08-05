using BlazorServer;
using IntegrationTests.Infrastructure;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.JSInterop;
using Microsoft.Playwright;

namespace KristofferStrube.Blazor.WebAudio.IntegrationTests.Infrastructure;

public enum Browser
{
    Firefox,
    Webkit,
    Chrome,
}

[TestFixture(Infrastructure.Browser.Firefox)]
[TestFixture(Infrastructure.Browser.Webkit)]
[TestFixture(Infrastructure.Browser.Chrome)]
public class BlazorTest(Browser browserName)
{
    private IHost? _host;

    protected Uri RootUri;

    protected JSRuntimeEvaluationContext EvaluationContext { get; set; } = default!;
    protected JSRuntimeEvaluationContext EvaluationContextCreator(IServiceProvider sp)
    {
        EvaluationContext = JSRuntimeEvaluationContext.Create(sp);
        return EvaluationContext;
    }

    protected IJSRuntime JSRuntime => EvaluationContext.JSRuntime;

    protected IPlaywright PlaywrightInstance { get; set; }
    protected IBrowser Browser { get; set; }
    protected IBrowserContext Context { get; set; }
    protected IPage Page { get; set; }

    [OneTimeSetUp]
    public async Task Setup()
    {
        PlaywrightInstance = await Playwright.CreateAsync();

        IBrowserType browserType = browserName switch
        {
            Infrastructure.Browser.Firefox => PlaywrightInstance.Firefox,
            Infrastructure.Browser.Webkit => PlaywrightInstance.Webkit,
            Infrastructure.Browser.Chrome => PlaywrightInstance.Chromium,
            _ => PlaywrightInstance.Chromium,
        };

        Browser = await browserType.LaunchAsync();
        // Create a new incognito browser context
        Context = await Browser.NewContextAsync();
        // Create a new page inside context.
        Page = await Context.NewPageAsync();

        _host = BlazorServer.Program.BuildWebHost([],
            serviceBuilder =>
            {
                _ = serviceBuilder
                    .AddScoped(typeof(EvaluationContext), EvaluationContextCreator);
            }
        );

        await _host.StartAsync();

        RootUri = new(_host.Services.GetRequiredService<IServer>().Features
            .GetRequiredFeature<IServerAddressesFeature>()
            .Addresses.Single());
    }

    [SetUp]
    public async Task SetupBeforeEachTestRun()
    {
        await OnAfterRerenderAsync();
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
        if (Page is not null)
        {
            await Page.CloseAsync();
        }
        if (Context is not null)
        {
            await Context.CloseAsync();
        }
        if (Browser is not null)
        {
            await Browser.CloseAsync();
        }
        if (PlaywrightInstance is not null)
        {
            PlaywrightInstance.Dispose();
        }
        if (_host is not null)
        {
            await _host.StopAsync();
            _host.Dispose();
        }
    }

    protected async Task OnAfterRerenderAsync()
    {
        _ = await Page.GotoAsync(RootUri.AbsoluteUri);
        await Assertions.Expect(Page.GetByTestId("result")).ToHaveTextAsync($"done");
    }
}