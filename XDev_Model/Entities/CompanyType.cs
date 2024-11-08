using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace XDev_Model.Entities
{
    public class CompanyType:AuditEntity
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public HashSet<Company> Companies { get; set; }
    }
}
