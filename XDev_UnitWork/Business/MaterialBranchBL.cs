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
            
            return await Task.Run<List<MaterialBranchListDTO>>(() =>
            {
                //var query = DbContext.Database.SqlQuery<MaterialBranchListDTO>($"select a.MaterialId, a.BranchId, b.Code as BranchCode, b.Name as BranchName, b.CompanyId, c.Name as CompanyName, a.PriceSale, a.PricePurchase, a.IsLockedPurchase, a.IsLockedSale, \r\n\tisnull(sum(d.Stock),0) as Stock, isnull(sum(d.SoldStock),0) as SoldStock, isnull(sum(d.PurchasedStock),0) as PurchasedStock, isnull(sum(d.LockedStock),0) as LockedStock, \r\n\tisnull(sum(d.InTransitStock),0) as InTransitStock, a.ConcurrencyStamp\r\nfrom MaterialBranch as a inner join Branch as b on b.id = a.BranchId\r\n\t\t\t\t\t     inner join Company as c on c.Id = b.CompanyId\r\n\t\t\t\t\t\t left join MaterialWareHouse as d on d.MaterialId = a.MaterialId and\r\n\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t d.BranchId = a.BranchId\r\nwhere a.MaterialId = {materialid.ToString()}\r\ngroup by a.MaterialId, a.BranchId, b.Code, b.Name, b.CompanyId, c.Name, a.PriceSale, a.PricePurchase, a.IsLockedPurchase, a.IsLockedSale, a.ConcurrencyStamp");
                var query = DbContext.Database.SqlQuery<MaterialBranchListDTO>($"SELECT\r\n  a.\"MaterialId\",\r\n  a.\"BranchId\",\r\n  b.\"Code\"           AS \"BranchCode\",\r\n  b.\"Name\"           AS \"BranchName\",\r\n  b.\"CompanyId\",\r\n  c.\"Name\"           AS \"CompanyName\",\r\n  a.\"PriceSale\",\r\n  a.\"PricePurchase\",\r\n  a.\"IsLockedPurchase\",\r\n  a.\"IsLockedSale\",\r\n  COALESCE(SUM(d.\"Stock\"),          0) AS \"Stock\",\r\n  COALESCE(SUM(d.\"SoldStock\"),      0) AS \"SoldStock\",\r\n  COALESCE(SUM(d.\"PurchasedStock\"), 0) AS \"PurchasedStock\",\r\n  COALESCE(SUM(d.\"LockedStock\"),    0) AS \"LockedStock\",\r\n  COALESCE(SUM(d.\"InTransitStock\"), 0) AS \"InTransitStock\",\r\n  a.\"ConcurrencyStamp\"\r\nFROM \"MaterialBranch\" AS a\r\nJOIN \"Branch\"          AS b ON b.\"Id\"        = a.\"BranchId\"\r\nJOIN \"Company\"         AS c ON c.\"Id\"        = b.\"CompanyId\"\r\nLEFT JOIN \"MaterialWareHouse\" AS d\r\n  ON d.\"MaterialId\"  = a.\"MaterialId\"\r\n AND d.\"BranchId\"    = a.\"BranchId\"\r\nWHERE a.\"MaterialId\" = {materialid.ToString()}::uuid\r\nGROUP BY\r\n  a.\"MaterialId\",\r\n  a.\"BranchId\",\r\n  b.\"Code\",\r\n  b.\"Name\",\r\n  b.\"CompanyId\",\r\n  c.\"Name\",\r\n  a.\"PriceSale\",\r\n  a.\"PricePurchase\",\r\n  a.\"IsLockedPurchase\",\r\n  a.\"IsLockedSale\",\r\n  a.\"ConcurrencyStamp\";\r\n");
                return query.ToListAsync();
            });

            //var query = await (from mb in DbContext.MaterialBranch.AsNoTracking()
            //                   join br in DbContext.Branch.AsNoTracking() on mb.BranchId equals br.Id
            //                   join co in DbContext.Company.AsNoTracking() on br.CompanyId equals co.Id
            //                   where mb.MaterialId == materialid.GetGuid()
            //                   select new MaterialBranchListDTO
            //                   {
            //                       MaterialId = mb.MaterialId,
            //                       BranchId = mb.BranchId,
            //                       BranchCode = br.Code,
            //                       BranchName = br.Name,
            //                       CompanyId = br.CompanyId,
            //                       CompanyName = co.Name,
            //                       PriceSale= mb.PriceSale,
            //                       PricePurchase= mb.PricePurchase,
            //                       IsLockedPurchase= mb.IsLockedPurchase,
            //                       IsLockedSale= mb.IsLockedSale, 
            //                       ConcurrencyStamp = mb.ConcurrencyStamp,
            //                   }).ToListAsync();


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
