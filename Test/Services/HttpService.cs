using System.Net;
using App.Models;
using App.Services;
using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace Test.Services;

public class Tests
{
    private MockRepository _mockRepository;
    private Fixture _fixture;

    [SetUp]
    public void Setup()
    {
        _mockRepository = new MockRepository(MockBehavior.Strict);
        _fixture = new Fixture();
    }

    [Test]
    public async Task GetFromJsonAsync_Should_Return_Result()
    {
        // Arrange
        var mockFactory = new Mock<IHttpClientFactory>();
        var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
        mockHttpMessageHandler.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\"results\": [{\"display_title\": \"Terminator Salvation\"}]}"),
            });

        var client = new HttpClient(mockHttpMessageHandler.Object);
        mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);

        var options = Options.Create(new ApiOptions
        {
            ApiKey = "123",
            BaseUrl = "https://www.api.com",
            MovieReviewsEndpoint = "/search?query={0}&key={1}"
        });

        var sut = new MovieReviewService(mockFactory.Object, options);

        // Act
        var response = await sut.GetFromJsonAsync("Terminator");

        // Assert
        response.Results.Should().BeEquivalentTo(new List<Result> {
            new Result
            {
                DisplayTitle = "Terminator Salvation"
            }
        });
    }
}