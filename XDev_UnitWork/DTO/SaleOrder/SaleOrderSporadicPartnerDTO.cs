using XDev_Model.Entities;

namespace XDev_UnitWork.DTO.SaleOrder
{
    public class SaleOrderSporadicPartnerDTO
    {
        public Guid Id { get; set; } 
        public Guid SaleOrderId { get; set; }        
        public string Name { get; set; } = string.Empty;
        public Guid IDTypeId { get; set; }
        public string IDNumber { get; set; } = string.Empty ;
        public string Address { get; set; } = string.Empty;
        public Guid? CountryId { get; set; }        
        public Guid? RegionId { get; set; }        
        public Guid? CityId { get; set; }        
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
