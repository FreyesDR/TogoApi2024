using XDev_Model.Entities;

namespace XDev_UnitWork.DTO.Partner
{
    public class PartnerInfoDTO
    {
        public string Code {  get; set; }
        public string Name { get; set; }
        public string TradeName { get; set; }
        public Guid Nif1Id { get; set; }
        public string Nif1Code { get; set; }
        public string Nif1 {  get; set; }
        public Guid Nif2Id { get; set; }
        public string Nif2Code { get; set; }
        public string Nif2 { get; set; }
        public string EcoActCode { get; set; }
        public string EcoActName { get; set; }
        public string Address { get; set; }
        public string CountryCode { get; set; }
        public string CountryName { get; set; }
        public string CountryAltCode {  get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string CityCode { get; set; }
        public string CityName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string TypePerson {  get; set; }
        public string ContactPersonName { get; set; }
        public string ContactPersonIDNumber { get; set; }
        public string ContactPersonPhone { get; set; }
        public string ContactPersonEmail { get; set; }
    }
}
