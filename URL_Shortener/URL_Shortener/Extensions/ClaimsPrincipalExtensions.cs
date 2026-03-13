using System.Security.Claims;

namespace URL_Shortener.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var claim = user?.FindFirstValue(ClaimTypes.NameIdentifier);
            if(int.TryParse(claim, out var userId))
            {
                return userId;
            }

            throw new UnauthorizedAccessException("User ID claim is missing or invalid.");
        }
    }
}
