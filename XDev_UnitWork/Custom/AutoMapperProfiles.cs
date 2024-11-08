using AutoMapper;
using XDev_Model.Entities;
using XDev_UnitWork.DTO;

namespace XDev_UnitWork.Custom
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ApplicationUser, UserInfoDTO>();

            CreateMap<CompanyType, CompanyTypeDTO>();
            CreateMap<CompanyTypeDTO, CompanyType>();
            CreateMap<Company, CompanyDTO>();
            CreateMap<CompanyDTO, Company>();

            CreateMap<CompanyID, CompanyIDDTO>();
            CreateMap<CompanyIDDTO, CompanyID>();

            CreateMap<CompanyEconomicActivity, CompanyEconomicActivityDTO>();
            CreateMap<CompanyEconomicActivityDTO, CompanyEconomicActivity>();

            CreateMap<AddressType, AddressTypeDTO>();
            CreateMap<AddressTypeDTO, AddressType>();

            CreateMap<BranchType, BranchTypeDTO>();
            CreateMap<BranchTypeDTO, BranchType>();

            CreateMap<Branch, BranchDTO>();
            CreateMap<BranchDTO, Branch>();

            CreateMap<PartnerType, PartnerTypeDTO>();
            CreateMap<PartnerTypeDTO, PartnerType>();

            CreateMap<Address, AddressDTO>();
            CreateMap<AddressDTO, Address>();

            CreateMap<AddressEmail, AddressEmailDTO>();
            CreateMap<AddressEmailDTO, AddressEmail>();

            CreateMap<AddressPhone, AddressPhoneDTO>();
            CreateMap<AddressPhoneDTO, AddressPhone>();

            CreateMap<Country, CountryDTO>();
            CreateMap<CountryDTO, Country>();

            CreateMap<Region, RegionDTO>();
            CreateMap<RegionDTO, Region>();

            CreateMap<City, CityDTO>();
            CreateMap<CityDTO, City>();

            CreateMap<IDType, IDTypeDTO>();
            CreateMap<IDTypeDTO, IDType>();

            CreateMap<EconomicActivity, EconomicActivityDTO>();
            CreateMap<EconomicActivityDTO, EconomicActivity>();

            CreateMap<AppLog, AppLogDTO>();
            CreateMap<AppLogDTO, AppLog>();

            CreateMap<WareHouse, WareHouseDTO>();
            CreateMap<WareHouseDTO, WareHouse>();
        }
    }
}
