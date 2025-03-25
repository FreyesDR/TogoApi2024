using Microsoft.AspNetCore.Http.HttpResults;
using XDev_UnitWork.DTO.Admin;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.DM;
using XDev_TogoApi.Code;
using XDev_UnitWork.DTO.Material;
using XDev_UnitWork.Business;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace XDev_TogoApi.EndPoints
{
    public static class UserEP
    {
        public static RouteGroupBuilder MapUser(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Usuario"));
            builder.MapGet("/{id}", GetById).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Usuario"));
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<UserCreateDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Usuario"));
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<UserDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Usuario"));
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Usuario"));
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IUsersBL userBL)
        {
            await userBL.DeleteAsync(id);
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(UserDTO dto, IUsersBL userBL)
        {
            await userBL.UpdateAsync(dto);
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(UserCreateDTO dto, IUsersBL userBL)
        {
            try
            {
                var result = await userBL.CreateAsync(dto);
                if (result.Succeeded)
                {
                    return TypedResults.Ok();
                }
                else
                {
                    var response = new ExceptionReturnDTO
                    {
                        StatusCode = StatusCodes.Status400BadRequest.ToString(),
                        Message = "Error creación",
                        Errors = new List<string>()
                    };

                    foreach (var item in result.Errors)
                    {
                        response.Errors.Add(item.Description);
                    }

                    return TypedResults.BadRequest(response);
                }
            }
            catch (CustomTogoException ex)
            {
                return TypedResults.BadRequest(new ExceptionReturnDTO
                {
                    StatusCode = StatusCodes.Status500InternalServerError.ToString(),
                    Message = ex.InnerException == null ? ex.Message : ex.InnerException.Message
                });
            }
        }

        private static async Task<Results<Ok<UserDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, IUsersBL usersBL)
        {
            return TypedResults.Ok(await usersBL.GetUserByIdAsync(id));
        }

        private static async Task<Results<Ok<List<UserListDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, IUsersBL userBL)
        {
            return TypedResults.Ok(await userBL.GetUsersListAsync(pagination));
        }
    }
}
