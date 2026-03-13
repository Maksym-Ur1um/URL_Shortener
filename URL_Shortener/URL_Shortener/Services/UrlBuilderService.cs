using URL_Shortener.Services.Interfaces;

namespace URL_Shortener.Services
{
    public class UrlBuilderService : IUrlBuilderService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UrlBuilderService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string BuildFullShortUrl(string shortCode)
        {
            var request = _httpContextAccessor.HttpContext?.Request;
            return $"{request.Scheme}://{request.Host}/{shortCode}";
        }
    }
}
