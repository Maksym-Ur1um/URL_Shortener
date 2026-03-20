using FluentAssertions;
using Moq;
using URL_Shortener.Data.Repository;
using URL_Shortener.Models;
using URL_Shortener.Services;

namespace URL_ShortenerTests
{
    public class UrlShortenerServiceTests
    {
        private readonly Mock<IUrlRepository> _repositoryMock;
        private readonly UrlShortenerService _service;

        public UrlShortenerServiceTests()
        {
            _repositoryMock = new Mock<IUrlRepository>();
            _service = new UrlShortenerService(_repositoryMock.Object);
        }

        [Theory]
        [InlineData(1, "b")]
        [InlineData(62, "ba")]
        [InlineData(0, "a")]
        public async Task ShortenLinkAsync_CheckEncoding_ReturnsExpectedCode(int mockId, string expectedCode)
        {
            var url = "https://test.com/" + mockId;
            _repositoryMock.Setup(r => r.GetByOriginalUrlAsync(It.IsAny<string>()))
                .ReturnsAsync((ShortenedUrl)null);
            _repositoryMock.Setup(r => r.Add(It.IsAny<ShortenedUrl>()))
                .Callback<ShortenedUrl>(u => u.Id = mockId);

            var result = await _service.ShortenLinkAsync(url, 1);

            result.ShortUrl.Should().Be(expectedCode);
        }

        [Fact]
        public async Task ShortenLinkAsync_EmptyUrl_ThrowsArgumentException()
        {
            Func<Task> act = async () => await _service.ShortenLinkAsync("", 1);

            await act.Should().ThrowAsync<ArgumentException>();
        }

        [Theory]
        [InlineData("javascript:alert('XSS')")]
        [InlineData("mailto:test@google.com")]
        [InlineData("not-a-url-at-all")]
        [InlineData("")]
        public async Task ShortenLinkAsync_InvalidOrDangerousUrl_ThrowsArgumentException(string invalidUrl)
        {
            Func<Task> act = async () => await _service.ShortenLinkAsync(invalidUrl, 1);

            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Wrong Link!");
        }

        [Fact]
        public async Task ShortenLinkAsync_DuplicateUrl_ThrowsInvalidOperationException()
        {
            var duplicateUrl = "https://test.com/exists";
            _repositoryMock.Setup(r => r.GetByOriginalUrlAsync(duplicateUrl))
                .ReturnsAsync(new ShortenedUrl { OriginalUrl = duplicateUrl, ShortUrl = "shortUrl" });

            Func<Task> act = async () => await _service.ShortenLinkAsync(duplicateUrl, 1);

            await act.Should().ThrowAsync<InvalidOperationException>()
                .WithMessage("Link is already exists!");
        }
    }
}