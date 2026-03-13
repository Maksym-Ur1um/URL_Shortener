using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text;
using URL_Shortener.Data;
using URL_Shortener.Data.Repository;
using URL_Shortener.DTOs;
using URL_Shortener.Models;
using URL_Shortener.Services.Interfaces;

namespace URL_Shortener.Services
{
    public class UrlShortenerService : IUrlShortenerService
    {
        private readonly IUrlRepository _repository;



        public UrlShortenerService(IUrlRepository repository)
        {
            _repository = repository;
        }

        public async Task<UrlResponseDto> ShortenLinkAsync(string originalUrl, int userId)
        {
            if (!Uri.TryCreate(originalUrl, UriKind.Absolute, out var uriResult) 
                || (uriResult.Scheme != Uri.UriSchemeHttp 
                && uriResult.Scheme != Uri.UriSchemeHttps))
            {
                throw new ArgumentException("Wrong Link!");
            }

            var existingUrl = await _repository.GetByOriginalUrlAsync(originalUrl);
            if (existingUrl != null)
            {
                throw new InvalidOperationException("Link is already exists!");
            }

            ShortenedUrl urlEntity = new ShortenedUrl
            { 
                UserId = userId,
                OriginalUrl = originalUrl, 
                ShortUrl = Guid.NewGuid().ToString() 
            };

            _repository.Add(urlEntity);
            await _repository.SaveChangesAsync();

            string generatedCode = EncodeBase62(urlEntity.Id);
            urlEntity.ShortUrl = generatedCode;
            await _repository.SaveChangesAsync();

            var urlResponse = new UrlResponseDto
            {
                OriginalUrl = urlEntity.OriginalUrl,
                ShortUrl = urlEntity.ShortUrl
            };
            
            return urlResponse;
        }

        private string EncodeBase62(int id)
        {
            if (id == 0)
            {
                return "a";
            }
            const string BASE62 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            const int MAX_ENCODED_LENGTH = 6;
            char[] buffer = new char[MAX_ENCODED_LENGTH];
            int currentIndex = MAX_ENCODED_LENGTH - 1;
            while (id > 0)
            {
                int tempValue = id % 62;
                buffer[currentIndex] = BASE62[tempValue];
                id /= 62;
                currentIndex--;
            }
            int startIndex = currentIndex + 1;
            int length = MAX_ENCODED_LENGTH - startIndex;

            return new string(buffer, startIndex, length);
        }
    }
}
