using XDev_UnitWork.DTO.DM;

namespace XDev_UnitWork.Interfaces
{
    public interface IEconomicActivityBL : IGenericBL<EconomicActivityDTO>
    {
        Task CreateFromXlsx(Stream stream);
    }
}
