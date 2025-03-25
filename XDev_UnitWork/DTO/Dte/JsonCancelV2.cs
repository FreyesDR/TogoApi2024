using System.ComponentModel.DataAnnotations;

namespace XDev_UnitWork.DTO.Dte
{
    public class JsonCancelV2
    {
        public FeCancelV2IdentificationDTO identificacion { get; set; } = new FeCancelV2IdentificationDTO();
        public FeCancelV2TransmitterDTO emisor { get; set; } = new FeCancelV2TransmitterDTO();
        public FeCancelV2DocumentDTO documento { get; set; } = new FeCancelV2DocumentDTO();
        public FeCancelV2ReasonDTO motivo { get; set; } = new FeCancelV2ReasonDTO();
    }

    public class FeCancelV2ReasonDTO
    {
        public short tipoAnulacion { get; set; }        
        public string motivoAnulacion { get; set; }
        public string nombreResponsable { get; set; }
        public string tipDocResponsable { get; set; }
        public string numDocResponsable { get; set; }
        public string nombreSolicita { get; set; }
        public string tipDocSolicita { get; set; }
        public string numDocSolicita { get; set; }
    }

    public class FeCancelV2DocumentDTO
    {
        public string tipoDte { get; set; }
        public string codigoGeneracion { get; set; }
        public string selloRecibido { get; set; }
        public string numeroControl { get; set; }
        public string fecEmi { get; set; }
        public decimal montoIva { get; set; }
        public string codigoGeneracionR { get; set; }
        public string tipoDocumento { get; set; }
        public string numDocumento { get; set; }
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
    }

    public class FeCancelV2TransmitterDTO
    {
        public string nit { get; set; }
        public string nombre { get; set; }
        public string tipoEstablecimiento { get; set; }
        public string nomEstablecimiento { get; set; }
        public string codEstableMH { get; set; }
        public string codEstable { get; set; } = null;
        public string codPuntoVentaMH { get; set; }
        public string codPuntoVenta { get; set; } = null;
        public string telefono { get; set; }
        public string correo { get; set; }
    }

    public class FeCancelV2IdentificationDTO
    {
        public short version { get; set; } = 2;
        public string ambiente { get; set; }
        public string codigoGeneracion { get; set; }
        public string fecAnula { get; set; }
        public string horAnula { get; set; }
    }
}
