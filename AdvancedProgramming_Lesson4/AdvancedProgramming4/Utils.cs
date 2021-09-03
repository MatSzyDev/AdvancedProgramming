using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace AdvancedProgramming4
{
    public class Utils
    {
        public static bool IsAdmin(HttpContext httpContext)
        {
            return httpContext.User.Claims.Any(x => x.Type == ClaimTypes.Email && x.Value == "admin@email.com");
        }
    }
}