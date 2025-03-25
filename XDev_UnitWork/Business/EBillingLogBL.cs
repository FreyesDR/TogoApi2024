using AutoMapper;
using Microsoft.EntityFrameworkCore;
using XDev_Model;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.ElectronicBilling;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class EBillingLogBL : IEBillingLogBL
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor contextAccessor;

        public EBillingLogBL(ApplicationDbContext dbContext, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.contextAccessor = contextAccessor;
        }

        public async Task<EBillingLogDTO> GetLogByInvoiceId(Guid id)
        {
            var list = await (from log in dbContext.EBillingLog.AsNoTracking()
                              join co in dbContext.Company.AsNoTracking() on log.CompanyId equals co.Id
                              join br in dbContext.Branch.AsNoTracking() on log.BranchId equals br.Id
                              join ps in dbContext.PointSale.AsNoTracking() on log.PointSaleId equals ps.Id
                              join dte in dbContext.EBillingDocument.AsNoTracking() on log.TipoDte equals dte.Code
                              where log.InvoiceId  == id
                              select new EBillingLogDTO
                              {
                                  Id = log.Id,
                                  DateTime = log.DateTime,
                                  CompanyCode = co.Code,
                                  BranchCode = br.Code,
                                  PointSaleCode = ps.Code,
                                  CodGen = log.CodGen,
                                  NumControl = log.NumControl,
                                  SelloRecibido = log.SelloRecibido,
                                  TipoDte = log.TipoDte,
                                  TipoDteName = dte.Name,
                                  Request = log.Request,
                                  Response = log.Response,
                                  ResponseDate = log.ResponseDate,
                                  ResponseStatus = log.ResponseStatus,
                                  ResponseMessage = log.ResponseMessage,
                                  ResponseStatusCode = log.ResponseStatusCode,
                                  StatusCode = log.StatusCode,
                                  Observaciones = log.Observaciones,
                              }).ToListAsync();

            if (list.Any()) return list.FirstOrDefault();

            throw new CustomTogoException("Log no existe");
        }

        public async Task<EBillingLogDTO> GetLogByIdAsync(Guid logId)
        {
            var list = await (from log in dbContext.EBillingLog.AsNoTracking()
                              join co in dbContext.Company.AsNoTracking() on log.CompanyId equals co.Id
                              join br in dbContext.Branch.AsNoTracking() on log.BranchId equals br.Id
                              join ps in dbContext.PointSale.AsNoTracking() on log.PointSaleId equals ps.Id
                              join dte in dbContext.EBillingDocument.AsNoTracking() on log.TipoDte equals dte.Code into dtes
                              from d in dtes.DefaultIfEmpty()
                              where log.Id == logId
                              select new EBillingLogDTO
                              {
                                  Id = log.Id,
                                  DateTime = log.DateTime,
                                  CompanyCode = co.Code,
                                  BranchCode = br.Code,
                                  PointSaleCode = ps.Code,                                  
                                  CodGen = log.CodGen,
                                  NumControl = log.NumControl,
                                  SelloRecibido = log.SelloRecibido,    
                                  TipoDte = log.TipoDte,
                                  TipoDteName = d.Name,
                                  Request = log.Request,
                                  Response = log.Response,
                                  ResponseDate = log.ResponseDate,
                                  ResponseStatus = log.ResponseStatus,
                                  ResponseMessage = log.ResponseMessage,
                                  ResponseStatusCode = log.ResponseStatusCode,
                                  StatusCode = log.StatusCode,
                                  Observaciones = log.Observaciones,
                              }).ToListAsync();

            if(list.Any()) return list.FirstOrDefault();

            throw new CustomTogoException("Log no existe");
        }

        public async Task<List<EBillingLogDTO>> GetPaginationAsync(PaginationDTO dto)
        {
            if (dto.SortField.IsNullOrEmpty())
            {
                dto.SortField = "DateTime";
                dto.SortOrder = OrderDirection.descending;
            }
            var query = (from log in dbContext.EBillingLog.AsNoTracking()
                         join co in dbContext.Company.AsNoTracking() on log.CompanyId equals co.Id
                         join br in dbContext.Branch.AsNoTracking() on log.BranchId equals br.Id
                         join ps in dbContext.PointSale.AsNoTracking() on log.PointSaleId equals ps.Id
                         join dte in dbContext.EBillingDocument.AsNoTracking() on log.TipoDte equals dte.Code into dtes
                         from d in dtes.DefaultIfEmpty()
                         select new EBillingLogDTO
                         {
                             Id = log.Id,
                             DateTime = log.DateTime,
                             CompanyCode = co.Code,
                             BranchCode = br.Code,
                             PointSaleCode = ps.Code,
                             TipoDte = d.Code,
                             ResponseStatus = log.ResponseStatus,
                             ResponseMessage = log.ResponseMessage,
                         });

            query = query.CreateFilterAndOrder(dto);
            return await query.CreatePaging<EBillingLogDTO, EBillingLogDTO>(dto, contextAccessor.HttpContext);
        }
    }
}
