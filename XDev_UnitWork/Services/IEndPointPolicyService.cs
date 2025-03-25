
namespace XDev_UnitWork.Services
{
    public interface IEndPointPolicyService
    {
        Task<string> GetPolicyEndpointAsync(string metodoHttp, string ruta);
    }
}