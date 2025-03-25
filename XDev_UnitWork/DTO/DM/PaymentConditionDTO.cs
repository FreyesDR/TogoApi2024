using XDev_Model.Entities;

namespace XDev_UnitWork.DTO.DM
{
    public class PaymentConditionDTO:AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string Name { get; set; }
        public string Tipo { get; set; }
        public string Plazo { get; set; }
        public int PlazoCount { get; set; }
        public List<MeanOfPaymentDTO> MeansOfPayment { get; set; }        
    }

    public class PaymentConditionListDTO
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Tipo { get; set; }
        public string TipoName {  get; set; }
        public string Plazo { get; set; }
        public string PlazoName {  get; set; }
        public int PlazoCount { get; set; }
    }
}
