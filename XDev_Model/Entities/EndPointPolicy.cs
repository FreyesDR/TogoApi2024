namespace XDev_Model.Entities
{
    public class EndPointPolicy:AuditEntity
    {
        public Guid Id { get; set; }
        public string MethodHttp { get; set; } // "GET", "POST", etc.
        public string MethodPath { get; set; } // Ej: "/api/usuarios/{id}"
        public string Description { get; set; }
        public string Module {  get; set; }
        public Guid PolicyId { get; set; }
        public Policy Policy { get; set; }
        public string PolicyParams {  get; set; }
    }
}
