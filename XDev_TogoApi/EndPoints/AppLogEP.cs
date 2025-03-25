
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class AppLogEP
    {
        public static RouteGroupBuilder MapAppLog(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Log Aplicación"));
            return builder;
        }

        private static async Task<Results<Ok<List<AppLogDTO>>,BadRequest>> GetAll(PaginationDTO dto, IAppLogBL appLog)
        {
            return TypedResults.Ok(await appLog.GetListAsync(dto));
        }
    }
}
