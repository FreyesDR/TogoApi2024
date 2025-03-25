using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using XDev_Model;
using XDev_Model.Entities;
using XDev_UnitWork.Custom;

namespace XDev_UnitWork.Services
{
    public class UserPolicyService : IUserPolicyService
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserPolicyService(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<bool> GetUserPolicyAsync(string userName, string policy)
        {
            var user = await userManager.FindByEmailAsync(userName);
            if (user is null)
                return false;

            var roles = await userManager.GetRolesAsync(user);
            if (roles is null) 
                return false;

            if(policy.IsNullOrEmpty())
                return false;


            if (policy.Contains(","))
            {
                var listP = policy.Split(',').ToList();
                return roles.Any(el => listP.Contains(el));
            }
            else
            {
                return roles.Any(roles => roles.Contains(policy));
            }
            
        }
    }
}
