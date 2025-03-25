using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.DM;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class IDTypeEP
    {
        public static RouteGroupBuilder MapIDType(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Tipo Documento Identificación"));
            builder.MapGet("/list", GetList).WithDescription("Listado").WithMetadata(new ModuleAttribute("Tipo Documento Identificación")); ;
            builder.MapGet("/{id}", GetById).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Tipo Documento Identificación")); ;
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<IDTypeDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Tipo Documento Identificación")); ;
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<IDTypeDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Tipo Documento Identificación")); ;
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Tipo Documento Identificación")); ;
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IIDTypeBL idTypeBL)
        {
            await idTypeBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(IDTypeDTO dto, IIDTypeBL idTypeBL)
        {
            try
            {
                await idTypeBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(IDTypeDTO dto, IIDTypeBL idTypeBL)
        {
            try
            {
                await idTypeBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<IDTypeDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, IIDTypeBL idTypeBL)
        {
            return TypedResults.Ok(await idTypeBL.GetByIdAsync(id.GetGuid()));
        }

        private static async Task<Results<Ok<List<IDTypeDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(IIDTypeBL idTypeBL)
        {
            return TypedResults.Ok(await idTypeBL.GetListAsync());
        }

        private static async Task<Results<Ok<List<IDTypeDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, IIDTypeBL idTypeBL)
        {
            return TypedResults.Ok(await idTypeBL.GetListAsync(pagination));
        }
    }
}
