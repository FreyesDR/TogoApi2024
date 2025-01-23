namespace XDev_Model.Entities
{
    public class SaleOrderSporadicPartner:AuditEntity
    {
        public Guid Id { get; set; }
        public Guid SaleOrderId { get; set; }
        public SaleOrder SaleOrder { get; set; }
        public string Name {  get; set; }
        public Guid IDTypeId { get; set; }
        public string IDNumber {  get; set; }
        public string Address { get; set; }
        public Guid? CountryId { get; set; }        
        public Guid? RegionId { get; set; }        
        public Guid? CityId { get; set; }        
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
