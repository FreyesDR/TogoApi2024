using XDev_Model.Entities;
using XDev_UnitWork.DTO.Address;

namespace XDev_UnitWork.Interfaces
{
    public interface IAddressBL : IGenericBL<AddressDTO>
    {
        Task<List<AddressDTO>> GetByBranchId(Guid branchid);
        Task<List<AddressDTO>> GetByCompanyId(Guid companyid);
        Task<List<AddressDTO>> GetByPartnerId(Guid partnerid);
        Task<List<AddressEmailDTO>> GetEmails(Guid addressid);
        Task<List<AddressPhoneDTO>> GetPhones(Guid addressid);
    }
}
