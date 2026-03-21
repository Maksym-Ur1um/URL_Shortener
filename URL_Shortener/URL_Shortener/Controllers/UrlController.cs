using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using URL_Shortener.DTOs;
using URL_Shortener.Extensions;
using URL_Shortener.Services.Interfaces;

namespace URL_Shortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AutoValidateAntiforgeryToken]
    public class UrlController : ControllerBase
    {
        private readonly IUrlShortenerService _urlShortenerService;
        private readonly IUrlManagementService _urlManagementService;

        public UrlController(IUrlShortenerService urlShortenerService, IUrlManagementService urlManagementService)
        {
            _urlShortenerService = urlShortenerService;
            _urlManagementService = urlManagementService;
        }

        [HttpPost]
        [Authorize]
        [EnableRateLimiting("UrlCreationLimit")]
        public async Task<IActionResult> CreateShortLink([FromBody] CreateUrlDto createUrlDto)
        {
            int userId = User.GetUserId();

            UrlResponseDto responseDto = await _urlShortenerService
                .ShortenLinkAsync(createUrlDto.OriginalUrl, userId);

            return Ok(responseDto);
        }

        [HttpGet("{shortCode}")]
        public async Task<IActionResult> RedirectToOriginal(string shortCode)
        {
            string? originalUrl = await _urlManagementService.GetOriginalUrlAsync(shortCode);
            if (originalUrl == null)
            {
                return NotFound();
            }

            return Redirect(originalUrl);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUrls()
        {
            var urlDtos = await _urlManagementService.GetAllUrlsAsync();

            return Ok(urlDtos);
        }

        [HttpGet("info/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUrlInfo(int id)
        {
            var detailsDto = await _urlManagementService.GetUrlDetailsAsync(id);
            if (detailsDto == null)
            {
                return NotFound();
            }
            return Ok(detailsDto);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUrl(int id)
        {
            int currentUserId = User.GetUserId();
            bool isAdmin = User.IsInRole("Admin");

            await _urlManagementService.DeleteUrlAsync(id, currentUserId, isAdmin);
            return NoContent();
        }
    }
}