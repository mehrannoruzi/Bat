using System;
using System.Linq;
using System.Security.Claims;

namespace Bat.Core
{
    public static class ClaimsExtension
    {
        public static int GetUserId_Str(this ClaimsPrincipal cp) => int.Parse(cp.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        public static Guid GetUserId(this ClaimsPrincipal cp) => Guid.Parse(cp.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
        public static string GetEmail(this ClaimsPrincipal cp) => cp.Claims.First(x => x.Type == ClaimTypes.Email).Value;
        public static string GetUsername(this ClaimsPrincipal cp) => cp.Claims.First(x => x.Type == ClaimTypes.Name).Value;
        public static string GetFullname(this ClaimsPrincipal cp) => cp.Claims.First(x => x.Type == "Fullname").Value;
        public static string GetPicture(this ClaimsPrincipal cp) => cp.Claims.First(x => x.Type == "Picture").Value;
        public static T GetCustomField<T>(this ClaimsPrincipal cp) where T : class
        {
            try
            {
                var claim = cp.Claims.FirstOrDefault(x => x.Type == "CustomField");
                if (claim == null) return null;
                return claim.Value.DeSerializeJson<T>();
            }
            catch
            {
                return null;
            }
        }
    }
}
