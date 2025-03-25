namespace XDev_UnitWork.DTO.DM
{
    public class MeanOfPaymentDTO:AuditEntityDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Code { get; set; }
        public string Name { get; set; }        
    }
}
