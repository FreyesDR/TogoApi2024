using XDev_UnitWork.DTO;

namespace XDev_UnitWork.Interfaces
{
    public interface IEconomicActivityBL : IGenericBL<EconomicActivityDTO>
    {
        Task CreateFromXlsx(Stream stream);
    }
}
