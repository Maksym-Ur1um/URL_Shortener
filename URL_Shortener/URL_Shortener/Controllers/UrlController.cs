using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using URL_Shortener.Data.Repository;
using URL_Shortener.DTOs;
using URL_Shortener.Services.Interfaces;
using URL_Shortener.Extensions;

namespace URL_Shortener.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> CreateShortLink([FromBody] CreateUrlDto createUrlDto)
        {
            try
            {
                int userId = User.GetUserId();

                UrlResponseDto responseDto = await _urlShortenerService
                    .ShortenLinkAsync(createUrlDto.OriginalUrl, userId);

                return Ok(responseDto);
            }
            catch (ArgumentException ex)
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
            try
            {
                int currentUserId = User.GetUserId();
                bool isAdmin = User.IsInRole("Admin");

                await _urlManagementService.DeleteUrlAsync(id, currentUserId, isAdmin);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }
    }
}