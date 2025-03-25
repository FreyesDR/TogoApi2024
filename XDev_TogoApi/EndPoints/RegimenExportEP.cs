using XDev_TogoApi.Code;
using XDev_UnitWork.DTO.DM;
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class RegimenExportEP
    {
        public static RouteGroupBuilder MapRegimenExport(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Régimen Exportación"));
            builder.MapGet("/list", GetList).WithDescription("Listado").WithMetadata(new ModuleAttribute("Régimen Exportación"));
            builder.MapGet("/{id}", GetById).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Régimen Exportación"));
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<RegimenExportDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Régimen Exportación"));
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<RegimenExportDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Régimen Exportación"));
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Régimen Exportación"));
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IRegimenExportBL regimenExportBL)
        {
            await regimenExportBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(RegimenExportDTO dto, IRegimenExportBL regimenExportBL)
        {
            try
            {
                await regimenExportBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(RegimenExportDTO dto, IRegimenExportBL regimenExportBL)
        {
            try
            {
                await regimenExportBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<RegimenExportDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, IRegimenExportBL regimenExportBL)
        {
            return TypedResults.Ok(await regimenExportBL.GetByIdAsync(id.GetGuid()));
        }

        private static async Task<Results<Ok<List<RegimenExportDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(IRegimenExportBL regimenExportBL)
        {
            return TypedResults.Ok(await regimenExportBL.GetListAsync());
        }

        private static async Task<Results<Ok<List<RegimenExportDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, IRegimenExportBL regimenExportBL)
        {
            return TypedResults.Ok(await regimenExportBL.GetListAsync(pagination));
        }
    }
}
