using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Address;
using XDev_UnitWork.DTO.Partner;

namespace XDev_UnitWork.Interfaces
{
    public interface IPartnerBL : IGenericBL<PartnerDTO>
    {
        Task<List<AddressDTO>> GetAddressAsync(PaginationDTO pagination, string partnerid);
        Task<AddressDTO> GetAddressById(string partnerid, string id);
        Task<PartnerDTO> GetByCodeAsync(string code, string companyid);
        Task<List<PartnerListDTO>> GetPartnerListAsync(PaginationDTO pagination);
    }
}
