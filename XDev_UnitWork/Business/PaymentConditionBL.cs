using AutoMapper;
using Microsoft.EntityFrameworkCore;

using XDev_Model;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.DM;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class PaymentConditionBL : GenericBL<IPaymentConditionRep>, IPaymentConditionBL
    {
        public PaymentConditionBL(ApplicationDbContext dbContext, IHttpContextAccessor contextAccessor, IMapper mapper) : base(dbContext, contextAccessor, mapper)
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

        public async Task CreateAsync(PaymentConditionDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Code == dto.Code);
            if (model is null)
            {
                model = Mapper.Map<PaymentCondition>(dto);
                await Repository.CreateAsync(model);
            }else throw new CustomTogoException($"El código [{model.Code}] ya existe");
        }

        public async Task DeleteAsync(Guid id)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Id == id);
            if (model is not null)
                await Repository.DeleteAsync(model);
        }

        public async Task<PaymentConditionDTO> GetByIdAsync(params object[] ids)
        {
            var model = await Repository.GetByIdAsync(ids);
            if (model is null)
                return new PaymentConditionDTO();

            return Mapper.Map<PaymentConditionDTO>(model);
        }

        public Task<List<PaymentConditionDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public async Task<List<PaymentConditionListDTO>> GetPaymentCondListAsync(PaginationDTO pagination)
        {
            var query = (from pc in DbContext.PaymentCondition.AsNoTracking()
                         select new PaymentConditionListDTO
                         {
                             Id = pc.Id,
                             Code = pc.Code,
                             Name = pc.Name,
                             Tipo = pc.Tipo,
                             TipoName = pc.Tipo == "1" ? "Contado" : "Crédito",
                             Plazo = pc.Plazo,
                             PlazoName = pc.Plazo == "01" ? "Días" : pc.Plazo == "02" ? "Meses" : "Años",
                             PlazoCount = pc.PlazoCount,
                         });

            query = query.CreateFilterAndOrder(pagination);
            return await query.CreatePaging<PaymentConditionListDTO, PaymentConditionListDTO>(pagination, ContextAccessor.HttpContext);
        }

        public async Task<List<PaymentConditionDTO>> GetListAsync()
        {
            var model = await Repository.GetListAsync();
            return Mapper.Map<List<PaymentConditionDTO>>(model.OrderBy(o => o.Code).ToList());
        }

        public async Task UpdateAsync(PaymentConditionDTO dto)
        {
            var model = await Repository.GetFirstorDefaultAsync(f => f.Id == dto.Id);

            try
            {
                if (model is not null)
                {
                    model.Name = dto.Name;
                    model.Tipo = dto.Tipo;
                    model.Plazo = dto.Plazo;
                    model.PlazoCount = dto.PlazoCount;

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
