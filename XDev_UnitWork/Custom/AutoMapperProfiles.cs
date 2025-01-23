using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using XDev_Model.Entities;
using XDev_UnitWork.DTO;
using XDev_UnitWork.DTO.Address;
using XDev_UnitWork.DTO.Company;
using XDev_UnitWork.DTO.DM;
using XDev_UnitWork.DTO.ElectronicBilling;
using XDev_UnitWork.DTO.Invoice;
using XDev_UnitWork.DTO.Material;
using XDev_UnitWork.DTO.Partner;
using XDev_UnitWork.DTO.PriceScheme;
using XDev_UnitWork.DTO.SaleOrder;

namespace XDev_UnitWork.Custom
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ApplicationUser, UserInfoDTO>();

            CreateMap<CompanyType, CompanyTypeDTO>().ReverseMap();
            //CreateMap<CompanyTypeDTO, CompanyType>();
            CreateMap<Company, CompanyDTO>().ReverseMap();
            //CreateMap<CompanyDTO, Company>();

            CreateMap<CompanyID, CompanyIDDTO>().ReverseMap();
            //CreateMap<CompanyIDDTO, CompanyID>();

            CreateMap<CompanyEconomicActivity, CompanyEconomicActivityDTO>().ReverseMap();
            //CreateMap<CompanyEconomicActivityDTO, CompanyEconomicActivity>();

            CreateMap<AddressType, AddressTypeDTO>().ReverseMap();
            //CreateMap<AddressTypeDTO, AddressType>();

            CreateMap<BranchType, BranchTypeDTO>().ReverseMap();
            //CreateMap<BranchTypeDTO, BranchType>();

            CreateMap<Branch, BranchDTO>().ReverseMap(); ;
            //CreateMap<BranchDTO, Branch>();

            CreateMap<PartnerType, PartnerTypeDTO>().ReverseMap();
            //CreateMap<PartnerTypeDTO, PartnerType>();

            CreateMap<PartnerFeatures, PartnerFeaturesDTO>().ReverseMap();
            //CreateMap<PartnerFeaturesDTO, PartnerFeatures>();

            CreateMap<PartnerRole, PartnerRoleDTO>().ReverseMap();
            //CreateMap<PartnerRoleDTO, PartnerRole>();

            CreateMap<Partner, PartnerListDTO>();
            CreateMap<Partner, PartnerDTO>().ReverseMap();
            //CreateMap<PartnerDTO, Partner>();

            CreateMap<PartnerID, PartnerIDDTO>().ReverseMap();
            //CreateMap<PartnerIDDTO, PartnerID>();

            CreateMap<PartnerEconomicActivity, PartnerEconomicActivityDTO>().ReverseMap();
            //CreateMap<PartnerEconomicActivityDTO, PartnerEconomicActivity>();

            CreateMap<PartnerRoles, PartnerRolesDTO>().ReverseMap();
            //CreateMap<PartnerRolesDTO, PartnerRoles>();

            CreateMap<PartnerCompany, PartnerCompanyDTO>();
            CreateMap<PartnerCompanyDTO, PartnerCompany>()
                .ForMember(m => m.Company, s => s.Ignore());

            CreateMap<Address, AddressDTO>().ReverseMap();
            //CreateMap<AddressDTO, Address>();

            CreateMap<AddressEmail, AddressEmailDTO>().ReverseMap();
            //CreateMap<AddressEmailDTO, AddressEmail>();

            CreateMap<AddressPhone, AddressPhoneDTO>().ReverseMap();
            //CreateMap<AddressPhoneDTO, AddressPhone>();

            CreateMap<Country, CountryDTO>().ReverseMap();
            //CreateMap<CountryDTO, Country>();

            CreateMap<Region, RegionDTO>().ReverseMap();
            //CreateMap<RegionDTO, Region>();

            CreateMap<City, CityDTO>().ReverseMap();
            //CreateMap<CityDTO, City>();

            CreateMap<IDType, IDTypeDTO>().ReverseMap();
            //CreateMap<IDTypeDTO, IDType>();

            CreateMap<EconomicActivity, EconomicActivityDTO>().ReverseMap();
            //CreateMap<EconomicActivityDTO, EconomicActivity>();

            CreateMap<AppLog, AppLogDTO>().ReverseMap();
            //CreateMap<AppLogDTO, AppLog>();

            CreateMap<WareHouse, WareHouseDTO>().ReverseMap();
            //CreateMap<WareHouseDTO, WareHouse>();

            CreateMap<PointSale, PointSaleDTO>().ReverseMap();
            //CreateMap<PointSaleDTO, PointSale>();

            CreateMap<NumberRange, NumberRangeDTO>().ReverseMap();
            //CreateMap<NumberRangeDTO, NumberRange>();

            CreateMap<Currency, CurrencyDTO>().ReverseMap();
            //CreateMap<CurrencyDTO, Currency>();

            CreateMap<InvoiceType, InvoiceTypeDTO>().ReverseMap();
            //CreateMap<InvoiceTypeDTO,InvoiceType>();

            CreateMap<SaleOrderType, SaleOrderTypeDTO>().ReverseMap();
            //CreateMap<SaleOrderTypeDTO, SaleOrderType>();

            CreateMap<SaleOrder, SaleOrderDTO>().ReverseMap();
            //CreateMap<SaleOrderDTO, SaleOrder>();

            CreateMap<SaleOrderPosition, SaleOrderPositionDTO>().ReverseMap();
            //CreateMap<SaleOrderPositionDTO, SaleOrderPosition>();

            CreateMap<SaleOrderPositionCondition, SaleOrderPositionConditionDTO>().ReverseMap();

            CreateMap<SaleOrderSporadicPartner, SaleOrderSporadicPartnerDTO>().ReverseMap();
            //CreateMap<SaleOrderSporadicPartnerDTO, SaleOrderSporadicPartner>();

            CreateMap<MaterialType, MaterialTypeDTO>().ReverseMap();
            //CreateMap<MaterialTypeDTO, MaterialType>();

            CreateMap<UnitMeasure, UnitMeasureDTO>().ReverseMap();
            //CreateMap<UnitMeasureDTO, UnitMeasure>();

            CreateMap<MaterialFeatures, MaterialFeatureDTO>().ReverseMap();
            //CreateMap<MaterialFeatureDTO, MaterialFeatures>();

            CreateMap<Material, MaterialDTO>().ReverseMap();
            //CreateMap<MaterialDTO, Material>();

            CreateMap<PriceCondition, PriceConditionDTO>().ReverseMap();

            CreateMap<PriceScheme, PriceSchemeDTO>().ReverseMap();
            CreateMap<PriceSchemeCondition, PriceSchemeConditionDTO>().ReverseMap();

            CreateMap<MaterialBranch, MaterialBranchDTO>().ReverseMap();            

            CreateMap<MaterialWareHouse, MaterialWareHouseDTO>().ReverseMap();

            CreateMap<EBilling, EBillingDTO>().ReverseMap();

            CreateMap<EBillingCompany, EBillingCompanyDTO>().ReverseMap();

            CreateMap<EBillingDocument, EBillingDocumentDTO>().ReverseMap();

            CreateMap<EBillingCompanyInvoice, EBillingCompanyInvoiceDTO>().ReverseMap();
        }
    }
}
