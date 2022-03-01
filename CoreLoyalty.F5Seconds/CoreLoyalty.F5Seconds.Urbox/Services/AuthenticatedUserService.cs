using CoreLoyalty.F5Seconds.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CoreLoyalty.F5Seconds.Urbox.Services
{
    public class AuthenticatedUserService : IAuthenticatedUserService
    {
        public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
        }

        public string UserId { get; }
    }
}
