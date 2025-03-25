using XDev_TogoApi.Code;
using XDev_UnitWork.DTO.DM;
using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class IncoTermsEP
    {
        public static RouteGroupBuilder MapIncoTerms(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Incoterms"));
            builder.MapGet("/list", GetList).WithDescription("Listado").WithMetadata(new ModuleAttribute("Incoterms"));
            builder.MapGet("/{id}", GetById).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Incoterms"));
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<IncoTermsDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Incoterms"));
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<IncoTermsDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Incoterms"));
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Incoterms"));
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IIncoTermsBL incoTermsBL)
        {
            await incoTermsBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(IncoTermsDTO dto, IIncoTermsBL incoTermsBL)
        {
            try
            {
                await incoTermsBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(IncoTermsDTO dto, IIncoTermsBL incoTermsBL)
        {
            try
            {
                await incoTermsBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<IncoTermsDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, IIncoTermsBL incoTermsBL)
        {
            return TypedResults.Ok(await incoTermsBL.GetByIdAsync(id.GetGuid()));
        }

        private static async Task<Results<Ok<List<IncoTermsDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(IIncoTermsBL incoTermsBL)
        {
            return TypedResults.Ok(await incoTermsBL.GetListAsync());
        }

        private static async Task<Results<Ok<List<IncoTermsDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, IIncoTermsBL incoTermsBL)
        {
            return TypedResults.Ok(await incoTermsBL.GetListAsync(pagination));
        }
    }
}
