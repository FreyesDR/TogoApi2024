using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Material;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class MaterialWareHouseBL : GenericBL<IMaterialWareHouseRep>, IMaterialWareHouseBL
    {
        public MaterialWareHouseBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
        {
        }

        public Task<bool> AnyAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(string code)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(MaterialWareHouseDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.MaterialId == dto.MaterialId && f.BranchId == dto.BranchId && f.WareHouseId == dto.WareHouseId );
            if (model is null)
            {
                model = Mapper.Map<MaterialWareHouse>(dto);
                await Repository.CreateAsync(model);
            }
            else throw new CustomTogoException("Almacén ya existe para esta sucursal y material");
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<MaterialWareHouseDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetByIdAsync(ids);
            return Mapper.Map<MaterialWareHouseDTO>(model);
        }

        public Task<List<MaterialWareHouseDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public Task<List<MaterialWareHouseDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<List<MaterialWareHouseListDTO>> GetListAsync(string materialid, string branchid)
        {
            var query = await (from mwh in DbContext.MaterialWareHouse.AsNoTracking()
                               join br in DbContext.Branch.AsNoTracking() on mwh.BranchId equals br.Id
                               join wh in DbContext.WareHouse.AsNoTracking() on mwh.WareHouseId equals wh.Id
                               where mwh.MaterialId == materialid.GetGuid() && mwh.BranchId == branchid.GetGuid()
                               select new MaterialWareHouseListDTO
                               {
                                   MaterialId = mwh.MaterialId,
                                   BranchId = mwh.BranchId,
                                   BranchName = br.Name,
                                   WareHouseId = mwh.WareHouseId,
                                   WareHouseCode = wh.Code,
                                   WareHouseName = wh.Name,
                                   Stock = mwh.Stock,
                                   SoldStock = mwh.SoldStock,
                                   PurchasedStock = mwh.PurchasedStock,
                                   LockedStock = mwh.LockedStock,
                                   InTransitStock = mwh.InTransitStock,
                               }).ToListAsync();

            return query;
        }

        public Task UpdateAsync(MaterialWareHouseDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
