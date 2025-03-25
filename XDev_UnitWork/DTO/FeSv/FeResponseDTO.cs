namespace XDev_UnitWork.DTO.FeSv
{
    public class FeResponseDTO
    {
        public string StatusCode { get; set; } = StatusCodes.Status400BadRequest.ToString();
        public string Message { get; set; } = "Error procesando solicitud";
        public DateTime Date { get; set; } = DateTime.Now;
        public ResponseMHDTO ResponseMH { get; set; }
        public List<string> Observaciones { get; set; } = new List<string>();
        public Guid LogId { get; set; }
        public Guid InvoiceId { get; set; }
    }

    public class ResponseMHDTO
    {
        public short version { get; set; }
        public string ambiente { get; set; }
        public short versionApp { get; set; }
        public string estado { get; set; }
        public string codigoGeneracion { get; set; }
        public string selloRecibido { get; set; }
        public string fhProcesamiento { get; set; }
        public string clasificaMsg { get; set; }
        public string codigoMsg { get; set; }
        public string descripcionMsg { get; set; }
    }

    public class SignerJsonDTO
    {
        public string nit { get; set; }
        public bool activo { get; set; } = true;
        public string passwordPri { get; set; }
        public dynamic dteJson { get; set; }
    }

    internal class ResponseFirmadorDTO
    {
        public string status { get; set; }
        public ResponseFirmadorBody body { get; set; }
    }

    internal class ResponseFirmadorBody
    {
        public string codigo { get; set; }
        public dynamic mensaje { get; set; }
    }

    internal class ResponseTokenDTO
    {
        public string status { get; set; }
        public ResponseTokenBodyDTO body { get; set; }
    }

    internal class ResponseTokenBodyDTO
    {
        public string estado { get; set; }
        public string fhProcesamiento { get; set; }
        public string codigoMsg { get; set; }
        public string descripcionMsg { get; set; }
        public string observaciones { get; set; }
        public string clasificaMsg { get; set; }
    }

    internal class RequestDte
    {
        public string ambiente { get; set; }
        public short idEnvio { get; set; } = 1;
        public short version { get; set; }
        public string tipoDte { get; set; }
        public string documento { get; set; }
        public string codigoGeneracion { get; set; }
    }

    internal class RequestCancelDte
    {
        public string ambiente { get; set; }
        public short idEnvio { get; set; } = 1;
        public short version { get; set; }        
        public string documento { get; set; }        
    }

    internal class ResponseDte
    {
        public short version { get; set; }
        public string ambiente { get; set; }
        public short versionApp { get; set; }
        public string estado { get; set; }
        public string codigoGeneracion { get; set; }
        public string selloRecibido { get; set; }
        public string fhProcesamiento { get; set; }
        public string clasificaMsg { get; set; }
        public string codigoMsg { get; set; }
        public string descripcionMsg { get; set; }
        public List<string> observaciones { get; set; }
    }
}
