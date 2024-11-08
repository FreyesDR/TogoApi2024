using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using XDev_Model.Entities;

namespace XDev_UnitWork.Custom
{
    public class UserClaimsTransformation: IClaimsTransformation
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserClaimsTransformation(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var identity = principal.Identities.FirstOrDefault(c => c.IsAuthenticated);
            if (identity is null) return principal;

            var user = await userManager.GetUserAsync(principal);
            if (user is null) return principal;

            return new ClaimsPrincipal(identity);
        }
    }
}
