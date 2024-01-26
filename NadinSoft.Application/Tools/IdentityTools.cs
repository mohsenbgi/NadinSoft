using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace NadinSoft.Application.Tools
{
    public static class IdentityTools
    {
        public static string GetUserIP(this HttpContext context)
        => context.Connection.RemoteIpAddress.ToString();

        public static string GetUrlReferer(this HttpRequest request)
            => request.Headers["Referer"].ToString();

        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
                return default;

            if (claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier) == null)
                return default;

            string userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
            if (string.IsNullOrEmpty(userId))
                return default;
            else
                return userId;
        }

        public static string GetUserId(this IPrincipal principal)
        {
            if (principal == null)
                return default;

            var user = (ClaimsPrincipal)principal;

            return user.GetUserId();
        }

        public static string GetEmail(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal != null)
                return claimsPrincipal.FindFirst(ClaimTypes.Email)?.Value?.ToString() ?? "";

            return "";
        }

        public static string GetEmail(this IPrincipal principal)
        {
            var user = (ClaimsPrincipal)principal;

            return user.GetEmail();
        }

        public static string GetMobile(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal != null)
                return claimsPrincipal.FindFirst(ClaimTypes.MobilePhone)?.Value?.ToString() ?? "";

            return "";
        }

        public static string GetMobile(this IPrincipal principal)
        {
            var user = (ClaimsPrincipal)principal;

            return user.GetMobile();
        }
    }
}
