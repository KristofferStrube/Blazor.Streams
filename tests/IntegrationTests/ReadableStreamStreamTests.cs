using FluentAssertions;
using KristofferStrube.Blazor.Streams;
using KristofferStrube.Blazor.WebIDL;

namespace IntegrationTests;

public class ReadableStreamStreamTests(Browser browserName) : BlazorTest(browserName)
{
    [Test]
    public async Task ReadAsync_ShouldNotReadMoreThanBufferSize()
    {
        // Arrange
        await using CountQueuingStrategy strategy = await CountQueuingStrategy.CreateAsync(JSRuntime, new QueuingStrategyInit(10));

        byte count = 0;

        await using ReadableStream stream = await ReadableStream.CreateAsync(JSRuntime,
            underlyingSource: new UnderlyingSource(JSRuntime)
            {
                Pull = async (ctr) =>
                {
                    await using ReadableStreamDefaultController controller = ((ReadableStreamDefaultController)ctr)!;
                    Uint8Array data = await Uint8Array.CreateAsync(JSRuntime, 2);
                    count++;
                    await data.FillAsync(count);
                    await controller.EnqueueAsync(data.JSReference);
                }
            },
            strategy: strategy);

        // Act
        byte[] firstBuffer = new byte[1];
        int bytesReadFirstTime = await stream.ReadAsync(firstBuffer);

        byte[] secondBuffer = new byte[2];
        int bytesReadSecondTime = await stream.ReadAsync(secondBuffer);

        byte[] thirdBuffer = new byte[2];
        int bytesReadThirdTime = await stream.ReadAsync(thirdBuffer);

        // Assert
        _ = bytesReadFirstTime.Should().Be(1);
        _ = firstBuffer.Should().BeEquivalentTo([1]);

        _ = bytesReadSecondTime.Should().Be(2);
        _ = secondBuffer.Should().BeEquivalentTo([1, 2]);

        _ = bytesReadSecondTime.Should().Be(2);
        _ = thirdBuffer.Should().BeEquivalentTo([2,3]);
    }

    [Test]
    public async Task ReadAsync_ShouldFillBuffer_EvenIfPullOnlyProducesPart()
    {
        // Arrange
        await using CountQueuingStrategy strategy = await CountQueuingStrategy.CreateAsync(JSRuntime, new QueuingStrategyInit(10));

        byte count = 0;

        await using ReadableStream stream = await ReadableStream.CreateAsync(JSRuntime,
            underlyingSource: new UnderlyingSource(JSRuntime)
            {
                Pull = async (ctr) =>
                {
                    ReadableStreamDefaultController controller = ((ReadableStreamDefaultController)ctr)!;
                    await using Uint8Array byteArray = await Uint8Array.CreateAsync(JSRuntime, 10);
                    count++;
                    await byteArray.FillAsync(count);
                    await controller.EnqueueAsync(byteArray.JSReference);
                }
            },
            strategy: strategy);

        // Act
        byte[] firstBuffer = new byte[15];
        int bytesReadFirstTime = await stream.ReadAsync(firstBuffer);

        byte[] secondBuffer = new byte[12];
        int bytesReadSecondTime = await stream.ReadAsync(secondBuffer);

        // Assert
        _ = bytesReadFirstTime.Should().BeLessThanOrEqualTo(15);
        _ = firstBuffer[..10].Should().AllBeEquivalentTo(1);

        _ = bytesReadSecondTime.Should().BeLessThanOrEqualTo(12);
        _ = secondBuffer[..10].Should().AllBeEquivalentTo(2);
    }
}
