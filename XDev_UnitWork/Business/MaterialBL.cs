using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Company;
using XDev_UnitWork.DTO.Material;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class MaterialBL : GenericBL<IMaterialRep>, IMaterialBL
    {
        public MaterialBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
        {
        }

        public async Task<bool> AnyAsync(Guid id)
        {
            return await Repository.AnyAsync(f => f.Id == id);
        }

        public async Task<bool> AnyAsync(string code)
        {
            return await Repository.AnyAsync(f => f.Code == code);
        }

        public async Task CreateAsync(MaterialDTO dto)
        {
            if (dto.NumType == 1)
            {
                var matcfg = DbContext.MaterialFeatures.AsNoTracking().FirstOrDefault();
                dto.Code = UtilsExtension.NumberNextRange(matcfg.RangeId, DbContext.Database.GetConnectionString()).ToString();
            }
            else
            {
                if (await Repository.AnyAsync(f => f.Code == dto.Code))
                    throw new CustomTogoException($"Código [{dto.Code}] ya existe");
            }

            var model = Mapper.Map<Material>(dto);
            model.MaterialType = null;
            await Repository.CreateAsync(model);
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Id == id);
            if (model is not null)
            {
                model.IsDeleted = true;

                await Repository.UpdateAsync(model);
            }
        }

        public async Task<MaterialDTO> GetByIdAsync(params object[] ids)
        {
            var feature = await DbContext.MaterialFeatures.FirstOrDefaultAsync();
            var dto = new MaterialDTO() { NumType = feature.NumType };

            var model = await Repository.GetFirstorDefaultAsync(f => f.Id == ids[0].ToString().GetGuid());
            if (model is null)
                return dto;

            dto = Mapper.Map<MaterialDTO>(model);
            dto.NumType = feature.NumType;

            return dto;
        }

        public async Task<MaterialSaleDTO> GetByCode(string code, string branchid)
        {
            //var model = await Repository.GetFirstorDefaultAsync(f => f.Code == code);
            var query = await (from mat in DbContext.Material.AsNoTracking()
                               join mty in DbContext.MaterialType.AsNoTracking() on mat.MaterialTypeId equals mty.Id
                               join um in DbContext.UnitMeasure.AsNoTracking() on mat.UnitMeasureId equals um.Id
                               join br in DbContext.MaterialBranch.AsNoTracking() on mat.Id equals br.MaterialId                               
                               where mat.Code == code && br.BranchId == branchid.GetGuid() && br.IsLockedSale == false
                               select new MaterialSaleDTO
                               {
                                   Id = mat.Id,
                                   Code = mat.Code,
                                   Name = mat.Name,
                                   MaterialTypeId = mat.MaterialTypeId,
                                   MaterialTypeCode = mty.Code,
                                   OldCode = mat.OldCode,
                                   Price = br.PriceSale == 0 ? mat.Price: br.PriceSale,
                                   PriceType = mat.PriceType,
                                   UnitMeasureId = mat.UnitMeasureId,
                                   UnitMeasureCode = um.Code,
                                   Active = mat.Active,
                                   IsDeleted = mat.IsDeleted,                                   
                               }).ToListAsync();

            var model = query.FirstOrDefault();

            if (model is null)
                return null;

            return Mapper.Map<MaterialSaleDTO>(model);
        }

        public Task<List<MaterialDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MaterialListDTO>> GetMaterialListAsync(PaginationDTO pagination)
        {
            var query = (from mat in DbContext.Material.AsNoTracking()
                         join matt in DbContext.MaterialType.AsNoTracking() on mat.MaterialTypeId equals matt.Id
                         join um in DbContext.UnitMeasure.AsNoTracking() on mat.UnitMeasureId equals um.Id
                         where mat.IsDeleted == false
                         select new MaterialListDTO
                         {
                             Id = mat.Id,
                             Code = mat.Code,
                             Name = mat.Name,
                             MaterialType = matt.Name,
                             OldCode = mat.OldCode,
                             Active = mat.Active,                             
                             UnitMeasureCode = um.Code
                         }
                         ).AsQueryable();

            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<MaterialListDTO, MaterialListDTO>(pagination, ContextAccessor.HttpContext);
        }

        public async Task<List<MaterialSaleDTO>> GetMaterialActiveListAsync(PaginationDTO pagination,string branchid)
        {
            var query = (from mat in DbContext.Material.AsNoTracking()
                         join matt in DbContext.MaterialType.AsNoTracking() on mat.MaterialTypeId equals matt.Id
                         join um in DbContext.UnitMeasure.AsNoTracking() on mat.UnitMeasureId equals um.Id
                         join mbr in DbContext.MaterialBranch.AsNoTracking() on mat.Id equals mbr.MaterialId                         
                         where mat.IsDeleted == false && mat.Active == true && mbr.BranchId == branchid.GetGuid() && mbr.IsLockedSale == false
                         select new MaterialSaleDTO
                         {
                             Id = mat.Id,
                             Code = mat.Code,
                             Name = mat.Name,
                             MaterialTypeName = matt.Name,
                             MaterialTypeCode= matt.Code,
                             MaterialTypeId = mat.MaterialTypeId,
                             OldCode = mat.OldCode,
                             Active = mat.Active,
                             Price =mbr.PriceSale == 0 ? mat.Price: mbr.PriceSale,
                             PriceType = mat.PriceType,
                             UnitMeasureCode= um.Code,
                             UnitMeasureId = mat.UnitMeasureId,                             
                         }
                         ).AsQueryable();

            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<MaterialSaleDTO, MaterialSaleDTO>(pagination, ContextAccessor.HttpContext);
        }

        public Task<List<MaterialDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(MaterialDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Id == dto.Id);
            try
            {
                if (model is not null)
                {
                    model.MaterialTypeId = dto.MaterialTypeId;
                    model.Name = dto.Name;
                    model.OldCode = dto.OldCode;
                    model.Price = dto.Price;
                    model.PriceType = dto.PriceType;
                    model.Active = dto.Active;
                    model.UnitMeasureId = dto.UnitMeasureId;

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
