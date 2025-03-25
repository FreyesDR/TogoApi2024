using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Security.Claims;
using XDev_Model.Entities;
using XDev_UnitWork.DTO;

namespace XDev_UnitWork.Custom
{
    public class UserClaimsTransformation : IClaimsTransformation
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

            var claims = await userManager.GetClaimsAsync(user);
            var claim = claims.FirstOrDefault(c => c.Type == ClaimTypes.UserData);

            if (claim is null)
            {
                claim = new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(new CurrentUserDTO
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                    IDNumber = user.IDNumber,
                    IDCode = user.IDCode,
                }));
                //await userManager.AddClaimAsync(user, claim);
                identity.AddClaim(claim);
            }



            return new ClaimsPrincipal(identity);
        }
    }
}
