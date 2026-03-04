using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using URL_Shortener.DTOs;
using URL_Shortener.Services.Interfaces;

namespace URL_Shortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {
        private IUrlShortenerService _urlShortenerService;

        public UrlController(IUrlShortenerService urlShortenerService)
        {
            _urlShortenerService = urlShortenerService;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateShortLink([FromBody] CreateUrlDto createUrlDto)
        {
            try
            {
                int userId = int.Parse(User?.FindFirstValue(ClaimTypes.NameIdentifier));
                UrlResponseDto responseDto = await _urlShortenerService.
                    ShortenLinkAsync(createUrlDto.OriginalUrl, userId);
                string shortUrl = $"{Request.Scheme}://{Request.Host}/{responseDto.ShortUrl}";
                responseDto.ShortUrl = shortUrl;
                return Ok(responseDto);
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
