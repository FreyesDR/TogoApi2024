namespace XDev_Model.Entities
{
    public class PaymentCondition: AuditEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }        
        public string Name { get; set; }  
        public string Tipo { get; set; }        
        public string Plazo { get; set; }
        public int PlazoCount { get; set; }        

        public HashSet<Partner> Partners { get; set; }
        public HashSet<SaleOrder> SaleOrders { get; set; }
        public HashSet<Invoice> Invoices { get; set; }
    }
}
