
using Microsoft.AspNetCore.Http.HttpResults;
using NPOI.SS.Formula.Functions;
using NPOI.Util;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Company;
using XDev_UnitWork.DTO.Material;
using XDev_UnitWork.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace XDev_TogoApi.EndPoints
{
    public static class MaterialEP
    {
        public static RouteGroupBuilder MapMaterial(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetPagination);
            builder.MapGet("/search/{branchid}", GetActives);
            builder.MapGet("/{id}", GetById);
            builder.MapGet("/code/{code}", GetByCode);            
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<MaterialDTO>>();
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<MaterialDTO>>();
            builder.MapDelete("/{id}", Delete);
            return builder;
        }

        private static async Task<Results<Ok<List<MaterialListDTO>>, BadRequest<ExceptionReturnDTO>>> GetPagination(PaginationDTO dto, IMaterialBL materialBL)
        {
            return TypedResults.Ok(await materialBL.GetMaterialListAsync(dto));
        }

        private static async Task<Results<Ok<List<MaterialSaleDTO>>, BadRequest<ExceptionReturnDTO>>> GetActives(string branchid, PaginationDTO dto, IMaterialBL materialBL)
        {
            return TypedResults.Ok(await materialBL.GetMaterialActiveListAsync(dto,branchid));
        }

        private static async Task<Results<Ok<MaterialSaleDTO>, NotFound<string>>> GetByCode(string code, string branchid, IMaterialBL materialBL)
        {
            var result = await materialBL.GetByCode(code, branchid);

            if (result is null)
                return TypedResults.NotFound($"Código de material [{code}] no existe o no está ampliado para esta sucursal");

            return TypedResults.Ok(result);
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IMaterialBL materialBL)
        {
            await materialBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }


        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(MaterialDTO dto, IMaterialBL materialBL)
        {
            try
            {
                await materialBL.UpdateAsync(dto);
                return TypedResults.Ok();
            }
            catch (CustomTogoException ex)
            {
                return TypedResults.BadRequest(new ExceptionReturnDTO
                {
                    StatusCode = StatusCodes.Status500InternalServerError.ToString(),
                    Message = ex.Message
                });
            }
        }


        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(MaterialDTO dto, IMaterialBL materialBL)
        {
            try
            {
                await materialBL.CreateAsync(dto);
                return TypedResults.Ok();
            }
            catch (CustomTogoException ex)
            {
                return TypedResults.BadRequest(new ExceptionReturnDTO
                {
                    StatusCode = StatusCodes.Status500InternalServerError.ToString(),
                    Message = ex.Message
                });
            }
        }


        private static async Task<Results<Ok<MaterialDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, IMaterialBL materialBL)
        {
            return TypedResults.Ok(await materialBL.GetByIdAsync(id.GetGuid()));
        }

        
    }
}
