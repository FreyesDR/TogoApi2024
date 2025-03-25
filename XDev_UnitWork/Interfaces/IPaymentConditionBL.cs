using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.DM;

namespace XDev_UnitWork.Interfaces
{
    public interface IPaymentConditionBL : IGenericBL<PaymentConditionDTO>
    {
        Task<List<PaymentConditionListDTO>> GetPaymentCondListAsync(PaginationDTO pagination);
    }
}
