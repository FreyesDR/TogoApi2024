using XDev_UnitWork.DTO.Material;

namespace XDev_UnitWork.Interfaces
{
    public interface IMaterialFeatureBL : IGenericBL<MaterialFeatureDTO>
    {
        Task<MaterialFeatureDTO> GetAsync();
    }
}
