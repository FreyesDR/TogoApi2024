namespace XDev_Model.Entities
{
    public class Policy
    {
        public Guid Id { get; set; }
        public string Name { get; set; }        
        public HashSet<EndPointPolicy> EndPointPolicies { get; set; }
    }
}
