using FluentAssertions;
using KristofferStrube.Blazor.Streams;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;
using Microsoft.JSInterop.Implementation;
using System.Reflection;

namespace IntegrationTests;

[TestFixture(Infrastructure.Browser.Firefox)]
[TestFixture(Infrastructure.Browser.Chrome)]
public class ReadableStreamAsyncIteableTests(Browser browserName) : BlazorTest(browserName)
{
    private ReadableStream readableStream = default!;

    [SetUp]
    public async Task CreateReadableStream()
    {
        byte i = 1;
        readableStream = await ReadableStream.CreateAsync(JSRuntime, new UnderlyingSource(JSRuntime)
        {
            Pull = async (controller) =>
            {
                if (controller is not ReadableStreamDefaultController defaultController)
                {
                    return;
                }
                double? size = await controller.GetDesiredSizeAsync();
                if (size > 0)
                {
                    if (i % 2 == 1)
                    {
                        await defaultController.EnqueueAsync($"hey {i++}!");
                    }
                    else
                    {
                        await defaultController.EnqueueAsync(i++);
                    }
                }
            },
        }, (QueuingStrategy?)null);
    }

    [Test]
    public async Task ValuesAsync_ShouldReturnAsyncIteratorThatCanReturnValues()
    {
        // Act
        await using AsyncIterator<ValueReference> iterator = await readableStream.ValuesAsync();

        // Assert
        await iterator.MoveNextAsync();
        ValueReference firstChunk = iterator.Current;
        string? firstChunkTypeName = await firstChunk.GetTypeNameAsync();
        string? firstChunkValue = await firstChunk.GetValueAsync<string>();

        await iterator.MoveNextAsync();

        ValueReference secondChunk = iterator.Current;
        string? secondChunkTypeName = await secondChunk.GetTypeNameAsync();
        float? secondChunkValue = (float?)await secondChunk.GetValueAsync();

        firstChunkTypeName.Should().Be("string");
        firstChunkValue.Should().Be("hey 1!");
        secondChunkTypeName.Should().Be("number");
        secondChunkValue.Should().Be(2);
    }

    [Test]
    public async Task ValuesAsync_WithPreventCancelOptionForStream_ShouldReturnAsyncIteratorThatDoesNotPropagateCancel()
    {
        // Act
        AsyncIterator<ValueReference> iterator = await readableStream.ValuesAsync(new()
        {
            PreventCancel = true
        });

        // Assert
        await iterator.MoveNextAsync();
        ValueReference firstChunk = iterator.Current;
        string? firstChunkTypeName = await firstChunk.GetTypeNameAsync();
        string? firstChunkValue = await firstChunk.GetValueAsync<string>();

        await iterator.ReturnAsync(); // Even though we have cancelled here, the new iterator can still continue.
        await iterator.DisposeAsync();
        iterator = await readableStream.ValuesAsync();

        await iterator.MoveNextAsync();

        ValueReference secondChunk = iterator.Current;
        string? secondChunkTypeName = await secondChunk.GetTypeNameAsync();
        float? secondChunkValue = (float?)await secondChunk.GetValueAsync();

        firstChunkTypeName.Should().Be("string");
        firstChunkValue.Should().Be("hey 1!");
        secondChunkTypeName.Should().Be("number");
        secondChunkValue.Should().Be(2);

        await iterator.DisposeAsync();
    }

    [Test]
    public async Task ValuesAsync_ShouldReturnIteratorThatDisposesElements_WhenConfiguredToDisposeThem()
    {
        // Act
        AsyncIterator<ValueReference> iterator = await readableStream.ValuesAsync(
            disposePreviousValueWhenMovingToNextValue: true);

        // Assert
        List<ValueReference> elements = [];
        for (int i = 0; i < 5; i++)
        {
            await iterator.MoveNextAsync();
            elements.Add(iterator.Current);
        }
        await iterator.DisposeAsync();

        elements.Should().AllSatisfy(element =>
        {
            IsDisposed(element.JSReference).Should().BeTrue();
        });
    }

    [Test]
    public async Task ValuesAsync_ShouldReturnIteratorThatDoesNotDisposeElements_WhenConfiguredToNotDisposeThem()
    {
        // Act
        AsyncIterator<ValueReference> iterator = await readableStream.ValuesAsync(
            disposePreviousValueWhenMovingToNextValue: false);

        // Assert
        List<ValueReference> elements = [];
        for (int i = 0; i < 5; i++)
        {
            await iterator.MoveNextAsync();
            elements.Add(iterator.Current);
        }
        await iterator.DisposeAsync();

        elements.Should().AllSatisfy(element =>
        {
            IsDisposed(element.JSReference).Should().BeFalse();
        });
    }

    private static bool IsDisposed(IJSObjectReference reference)
    {
        PropertyInfo disposedProperty = typeof(JSObjectReference).GetProperty("Disposed", BindingFlags.Instance | BindingFlags.NonPublic)!;
        bool value = (bool)disposedProperty.GetValue(reference, null)!;
        return value;
    }
}
