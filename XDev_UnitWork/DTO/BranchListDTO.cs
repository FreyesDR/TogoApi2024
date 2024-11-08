namespace XDev_UnitWork.DTO
{
    public class BranchListDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string BranchType { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
