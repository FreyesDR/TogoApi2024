
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class DashBoardEP
    {
        public static RouteGroupBuilder MapDashBoard(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetData).WithDescription("Datos generales").WithMetadata(new ModuleAttribute("Dashboard"));
            builder.MapGet("/sales", GetSales).WithDescription("Datos venta").WithMetadata(new ModuleAttribute("Dashboard"));
            return builder;
        }

        private static async Task<Results<Ok<DashBoardSaleDTO>, BadRequest<ExceptionReturnDTO>>> GetSales(string days, IDashBoardBL dashBoardBL)
        {
            return TypedResults.Ok(await dashBoardBL.GetSales(Convert.ToInt32(days)));
        }

        private static async Task<Results<Ok<DashBoardDTO>, BadRequest<ExceptionReturnDTO>>> GetData(IDashBoardBL dashBoardBL)
        {
            return TypedResults.Ok(await dashBoardBL.GetDashboardDataAsync());
        }
    }
}
