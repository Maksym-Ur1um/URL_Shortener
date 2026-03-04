using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using URL_Shortener.DTOs;
using URL_Shortener.Repository;
using URL_Shortener.Services.Interfaces;

namespace URL_Shortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UrlController : ControllerBase
    {
        private IUrlShortenerService _urlShortenerService;
        private readonly IUrlRepository _repository;

        public UrlController(IUrlShortenerService urlShortenerService, IUrlRepository repository)
        {
            _urlShortenerService = urlShortenerService;
            _repository = repository;
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
        [HttpGet("{shortCode}")]
        public async Task<IActionResult> RedirectToOriginal(string shortCode)
        {
            var urlEntity = await _repository.GetByShortUrlAsync(shortCode);
            if (urlEntity == null)
            {
                return NotFound();
            }

            return Redirect(urlEntity.OriginalUrl);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUrls()
        {
            var urls = await _repository.GetAllUrlsAsync();
            var urlDtos = urls.Select(u => new UrlTableItemDto
            {
                Id = u.Id,
                OriginalUrl = u.OriginalUrl,
                ShortUrl = u.ShortUrl,
                CreatorId = u.UserId
            }).ToList();

            return Ok(urlDtos);
        }

        [HttpGet("info/{id}")]
        [Authorize]
        public async Task<IActionResult> GetUrlInfo(int id)
        {
            var urlEntity = await _repository.GetByIdAsync(id);
            if (urlEntity == null)
            {
                return NotFound();
            }

            var detailsDto = new UrlDetailsDto
            {
                OriginalUrl = urlEntity.OriginalUrl,
                ShortUrl = urlEntity.ShortUrl,
                CreatedAt = urlEntity.CreatedAt,
                CreatorName = urlEntity.User?.UserName
            };
            return Ok(detailsDto);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteUrl(int id)
        {
            var urlEntity = await _repository.GetByIdAsync(id);

            if (urlEntity == null)
            {
                return NotFound();
            }

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!int.TryParse(userIdClaim, out int currentUserId))
            {
                return Unauthorized();
            }
            if (urlEntity.UserId != currentUserId && !User.IsInRole("Admin"))
            {
                return Forbid();
            }

            _repository.Delete(urlEntity);
            await _repository.SaveChangesAsync();

            return NoContent();
        }

    }
}
