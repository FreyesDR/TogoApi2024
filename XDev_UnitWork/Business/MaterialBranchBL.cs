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
    public class MaterialBranchBL : GenericBL<IMaterialBranchRep>, IMaterialBranchBL
    {
        public MaterialBranchBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public async Task CreateAsync(MaterialBranchDTO dto)
        {
            var model = await Repository.GetByIdAsync(dto.MaterialId, dto.BranchId);
            if (model is null)
            {
                model = Mapper.Map<MaterialBranch>(dto);
                await Repository.CreateAsync(model);
            }
            else throw new CustomTogoException("El material ya fue ampliado para esta sucursal");
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<MaterialBranchDTO> GetByIdAsync(params object[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<MaterialBranchDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MaterialBranchListDTO>> GetListAsync(string materialid)
        {
            var query = await (from mb in DbContext.MaterialBranch.AsNoTracking()
                               join br in DbContext.Branch.AsNoTracking() on mb.BranchId equals br.Id
                               join co in DbContext.Company.AsNoTracking() on br.CompanyId equals co.Id
                               where mb.MaterialId == materialid.GetGuid()
                               select new MaterialBranchListDTO
                               {
                                   MaterialId = mb.MaterialId,
                                   BranchId = mb.BranchId,
                                   BranchName = br.Name,
                                   CompanyId = br.CompanyId,
                                   CompanyName = co.Name,
                                   PriceSale= mb.PriceSale,
                                   PricePurchase= mb.PricePurchase,
                                   IsLockedPurchase= mb.IsLockedPurchase,
                                   IsLockedSale= mb.IsLockedSale, 
                                   ConcurrencyStamp = mb.ConcurrencyStamp,
                               }).ToListAsync();

            return query;
        }

        public Task<List<MaterialBranchDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(MaterialBranchDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.MaterialId == dto.MaterialId && f.BranchId == dto.BranchId);
            try
            {
                if (model is not null)
                {
                    model.PriceSale = dto.PriceSale;
                    model.IsLockedPurchase = dto.IsLockedPurchase;
                    model.IsLockedSale = dto.IsLockedSale;

                    await Repository.UpdateAsync(model, dto.ConcurrencyStamp);
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                var user = await DbContext.Users.AsNoTracking().FirstOrDefaultAsync(f => f.Id == model.LastUpdatedBy);
                throw new CustomTogoException($"El registro fue modificado por el usuario '{user.UserName}'");
            }
        }
    }
}
