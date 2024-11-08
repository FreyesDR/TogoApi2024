namespace XDev_UnitWork.DTO
{
    public class ExceptionReturnDTO
    {
        public string Type { get; set; } = "error";
        public string Message { get; set; } = "Solicitud incorrecta";
        public string StatusCode { get; set; } = StatusCodes.Status400BadRequest.ToString();
        public IList<string> Errors { get; set; } = new List<string>();
    }
}
