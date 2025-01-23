using XDev_UnitWork.DTO.Partner;

namespace XDev_UnitWork.Interfaces
{
    public interface IPartnerRolesBL : IGenericBL<PartnerRolesDTO>
    {
        Task DeleteAsync(Guid partnerid, Guid roleid);
        Task<List<PartnerRolesDTO>> GetListAsync(Guid partnerId);
    }
}
