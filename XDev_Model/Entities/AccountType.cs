namespace XDev_Model.Entities
{
    public class AccountType:AuditEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }        
        public HashSet<AccountCatalog> Catalogs { get; set; }
    }
}
