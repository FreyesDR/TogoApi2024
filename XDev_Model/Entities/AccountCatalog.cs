namespace XDev_Model.Entities
{
    public class AccountCatalog: AuditEntity
    {
        public Guid Id { get; set; }    
        public string Code { get; set; }
        public string Name { get; set; }
        public Int16 Level { get; set; } // 1 = mayor, 2 = detalle
        public Guid? ParentId {  get; set; }
        public AccountCatalog ParentCatalog { get; set; }
        public HashSet<AccountCatalog> ChildrenCatalog { get; set; }
        public bool Active { get; set; }
        public Guid AccountTypeId { get; set; }
        public AccountType AccountType { get; set; }
    }
}
