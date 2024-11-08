namespace XDev_UnitWork.DTO
{
    public class AppLogDTO
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
