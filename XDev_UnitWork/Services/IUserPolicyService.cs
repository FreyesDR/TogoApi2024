
namespace XDev_UnitWork.Services
{
    public interface IUserPolicyService
    {
        Task<bool> GetUserPolicyAsync(string userName, string policy);
    }
}