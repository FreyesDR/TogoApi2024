using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Partner;

namespace XDev_UnitWork.Interfaces
{
    public interface IPartnerCompanyBL:IGenericBL<PartnerCompanyDTO>
    {
        Task DeleteAsync(Guid partnerid, Guid companyid);
        Task<List<PartnerCompanyDTO>> GetListAsync(Guid partnerid);
        Task<List<PartnerListDTO>> GetPartnerCompanyListAsync(PaginationDTO pagination, string companyid);
    }
}
