using XDev_TogoApi.Code;
using XDev_UnitWork.DTO.DM;
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class RecintoFiscalEP
    {
        public static RouteGroupBuilder MapRecintoFiscal(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Recinto Fiscal"));
            builder.MapGet("/list", GetList).WithDescription("Listado").WithMetadata(new ModuleAttribute("Recinto Fiscal"));
            builder.MapGet("/{id}", GetById).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Recinto Fiscal"));
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<RecintoFiscalDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Recinto Fiscal"));
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<RecintoFiscalDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Recinto Fiscal"));
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Recinto Fiscal"));
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IRecintoFiscalBL recintoFiscalBL)
        {
            await recintoFiscalBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(RecintoFiscalDTO dto, IRecintoFiscalBL recintoFiscalBL)
        {
            try
            {
                await recintoFiscalBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(RecintoFiscalDTO dto, IRecintoFiscalBL recintoFiscalBL)
        {
            try
            {
                await recintoFiscalBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<RecintoFiscalDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, IRecintoFiscalBL recintoFiscalBL)
        {
            return TypedResults.Ok(await recintoFiscalBL.GetByIdAsync(id.GetGuid()));
        }

        private static async Task<Results<Ok<List<RecintoFiscalDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(IRecintoFiscalBL recintoFiscalBL)
        {
            return TypedResults.Ok(await recintoFiscalBL.GetListAsync());
        }

        private static async Task<Results<Ok<List<RecintoFiscalDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, IRecintoFiscalBL recintoFiscalBL)
        {
            return TypedResults.Ok(await recintoFiscalBL.GetListAsync(pagination));
        }
    }
}
