using XDev_UnitWork.DTO.Partner;

namespace XDev_UnitWork.Interfaces
{
    public interface IPartnerFeaturesBL : IGenericBL<PartnerFeaturesDTO>
    {
        Task<PartnerFeaturesDTO> GetAsync();
    }
}
