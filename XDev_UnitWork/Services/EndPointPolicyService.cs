using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using XDev_Model;

namespace XDev_UnitWork.Services
{
    public class EndPointPolicyService: IEndPointPolicyService
    {
        private readonly ApplicationDbContext dbContext;

        public EndPointPolicyService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<string> GetPolicyEndpointAsync(string metodoHttp, string ruta)
        {
            var ep = await dbContext.EndPointPolicy.AsNoTracking().FirstOrDefaultAsync(f => f.MethodHttp == metodoHttp && f.MethodPath == ruta );
            if (ep is null) return string.Empty;

            return ep.PolicyParams;            
        }
    }
}
