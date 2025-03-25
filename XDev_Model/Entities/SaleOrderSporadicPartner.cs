namespace XDev_Model.Entities
{
    public class SaleOrderSporadicPartner:AuditEntity
    {
        public Guid Id { get; set; }
        public Guid SaleOrderId { get; set; }
        public SaleOrder SaleOrder { get; set; }
        public string Name {  get; set; }
        
        public Guid IDTypeId { get; set; }
        public string IDCode { get; set; }
        public string IDNumber {  get; set; }

        public Guid IDTypeId2 { get; set; }
        public string IDCode2 { get; set; }
        public string IDNumber2 { get; set; }

        public string Address { get; set; }
        public Guid? CountryId { get; set; }        
        public string CountryName { get; set; }
        public string CountryCode {  get; set; }
        public Guid? RegionId { get; set; }     
        public string RegionName { get; set; }
        public string RegionCode { get; set; }
        public Guid? CityId { get; set; }     
        public string CityName { get; set; }
        public string CityCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid EcoActivityId {  get; set; }
        public string EcoActivityName { get;set; }
        public string EcoActivityCode { get;set; }
        public string TypePerson {  get; set; }
    }
}
