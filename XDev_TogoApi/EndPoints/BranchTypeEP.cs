using Microsoft.AspNetCore.Http.HttpResults;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Company;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class BranchTypeEP
    {
        public static RouteGroupBuilder MapBranchType(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll);
            builder.MapGet("/list", GetList);
            builder.MapGet("/{id}", GetById);
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<BranchTypeDTO>>(); ;
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<BranchTypeDTO>>();
            builder.MapDelete("/{id}", Delete);
            return builder;
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IBranchTypeBL branchTypeBL)
        {
            await branchTypeBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(BranchTypeDTO dto, IBranchTypeBL branchTypeBL)
        {
            try
            {
                await branchTypeBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(BranchTypeDTO dto, IBranchTypeBL branchTypeBL)
        {
            try
            {
                await branchTypeBL.CreateAsync(dto);
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

        private static async Task<Results<Ok<BranchTypeDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string id, IBranchTypeBL branchTypeBL)
        {
            return TypedResults.Ok(await branchTypeBL.GetByIdAsync(id.GetGuid()));
        }

        private static async Task<Results<Ok<List<BranchTypeDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(IBranchTypeBL branchTypeBL)
        {
            return TypedResults.Ok(await branchTypeBL.GetListAsync());
        }

        private static async Task<Results<Ok<List<BranchTypeDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, IBranchTypeBL branchTypeBL)
        {
            return TypedResults.Ok(await branchTypeBL.GetListAsync(pagination));
        }
    }
}
