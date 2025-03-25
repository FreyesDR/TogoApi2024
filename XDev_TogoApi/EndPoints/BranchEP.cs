using Microsoft.AspNetCore.Http.HttpResults;
using XDev_Model.Entities;
using XDev_TogoApi.Code;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Address;
using XDev_UnitWork.DTO.Company;
using XDev_UnitWork.Interfaces;

namespace XDev_TogoApi.EndPoints
{
    public static class BranchEP
    {
        public static RouteGroupBuilder MapBranch(this RouteGroupBuilder builder)
        {
            builder.MapGet("/", GetAll).WithDescription("Listar todo").WithMetadata(new ModuleAttribute("Sucursal"));
            builder.MapGet("/list", GetListAll).WithDescription("Listado").WithMetadata(new ModuleAttribute("Sucursal"));
            builder.MapGet("/{companyid}/{id}", GetById).WithDescription("Obtener por Id").WithMetadata(new ModuleAttribute("Sucursal"));
            builder.MapGet("/{companyid}/list", GetList).WithDescription("Listado por Sociedad").WithMetadata(new ModuleAttribute("Sucursal"));
            builder.MapPost("/", Create).AddEndpointFilter<ValidationFilter<BranchDTO>>()
                                        .WithDescription("Crear").WithMetadata(new ModuleAttribute("Sucursal"));
            builder.MapPut("/", Update).AddEndpointFilter<ValidationFilter<BranchDTO>>()
                                       .WithDescription("Modificar").WithMetadata(new ModuleAttribute("Sucursal"));
            builder.MapDelete("/{id}", Delete).WithDescription("Eliminar").WithMetadata(new ModuleAttribute("Sucursal"));

            builder.MapGet("/{branchid}/address", GetAddresses).WithDescription("Listar Direcciones").WithMetadata(new ModuleAttribute("Sucursal"));
            builder.MapGet("/{branchid}/address/{id}", GetAddress).WithDescription("Obtener dirección por Id").WithMetadata(new ModuleAttribute("Sucursal"));

            
            return builder;
        }

        private static async Task<Results<Ok<List<BranchDTO>>, BadRequest<ExceptionReturnDTO>>> GetListAll(IBranchBL branchBL)
        {
            return TypedResults.Ok(await branchBL.GetListAsync());
        }

        private static async Task<Results<Ok<List<BranchListDTO>>, BadRequest<ExceptionReturnDTO>>> GetList(string companyid, IBranchBL branchBL)
        {
            return TypedResults.Ok(await branchBL.GetListAsync(companyid));
        }

        private static async Task<Results<Ok<List<BranchListDTO>>, BadRequest<ExceptionReturnDTO>>> GetAll(PaginationDTO pagination, string companyid, IBranchBL branchBL)
        {
            return TypedResults.Ok(await branchBL.GetBranchListAsync(pagination, companyid));
        }

        //private static async Task<Results<Ok<List<PointSaleDTO>>, BadRequest<ExceptionReturnDTO>>> GetPointSale(PaginationDTO pagination, string branchid, IPointSaleBL pointSaleBL)
        //{
        //    return TypedResults.Ok(await pointSaleBL.GetPointSaleListAsync(pagination, branchid));
        //}

        

        private static async Task<Results<Ok<AddressDTO>, BadRequest<ExceptionReturnDTO>>> GetAddress(string id, string branchid, IBranchBL branchBL)
        {
            return TypedResults.Ok(await branchBL.GetAddressById(branchid, id));
        }

        private static async Task<Results<Ok<List<AddressDTO>>, BadRequest<ExceptionReturnDTO>>> GetAddresses(PaginationDTO pagination, string branchid, IBranchBL branchBL)
        {
            return TypedResults.Ok(await branchBL.GetAddressAsync(pagination, branchid));
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Delete(string id, IBranchBL branchBL)
        {
            await branchBL.DeleteAsync(id.GetGuid());
            return TypedResults.Ok();
        }

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Update(BranchDTO dto, IBranchBL branchBL)
        {
            try
            {
                await branchBL.UpdateAsync(dto);
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

        private static async Task<Results<Ok, BadRequest<ExceptionReturnDTO>>> Create(BranchDTO dto, IBranchBL branchBL)
        {
            try
            {
                await branchBL.CreateAsync(dto);
                return TypedResults.Ok();
            }
            catch(CustomTogoException ex)
            {
                return TypedResults.BadRequest(new ExceptionReturnDTO
                {
                    StatusCode = StatusCodes.Status500InternalServerError.ToString(),
                    Message = ex.Message
                });
            }            
        }

        private static async Task<Results<Ok<BranchDTO>, BadRequest<ExceptionReturnDTO>>> GetById(string companyid, string id, IBranchBL branchBL)
        {
            return TypedResults.Ok(await branchBL.GetByIdAsync(companyid.GetGuid(), id.GetGuid()));
        }
    }
}
