using Microsoft.EntityFrameworkCore;

namespace XDev_UnitWork.DTO.Dte
{
    public class JsonFexV1
    {
        public FeFexV1IdentificacionDTO identificacion { get; set; } = new FeFexV1IdentificacionDTO();
        public FeFexV1EmisorDTO emisor { get; set; } = new FeFexV1EmisorDTO();
        public FeFexV1ReceiverDTO receptor { get; set; } = new FeFexV1ReceiverDTO();
        public List<FeFexV1OthersDocumentsDTO> otrosDocumentos { get; set; }
        public FeVentaTerceroDTO ventaTercero { get; set; } = null;
        public List<FeFexV1DetailDTO> cuerpoDocumento { get; set; } = new List<FeFexV1DetailDTO>();
        public FeFexV1SummaryDTO resumen { get; set; } = new FeFexV1SummaryDTO();
        public List<FeAppendixDTO> apendice { get; set; } = null;
    }

    public class FeFexV1SummaryDTO
    {
        public decimal totalGravada { get; set; }
        public decimal descuento { get; set; }
        public decimal porcentajeDescuento { get; set; }
        public decimal totalDescu { get; set; }
        public decimal seguro { get; set; }
        public decimal flete { get; set; }
        public decimal montoTotalOperacion { get; set; }
        public decimal totalNoGravado { get; set; }
        public decimal totalPagar => montoTotalOperacion + totalNoGravado;
        public string totalLetras { get; set; }
        public int condicionOperacion { get; set; }
        public List<FePagosDTO> pagos { get; set; } = new List<FePagosDTO>();
        public string codIncoterms { get; set; }
        public string descIncoterms { get; set; }
        public string numPagoElectronico { get; set; }
        public string observaciones { get; set; }
    }

    public class FeFexV1DetailDTO
    {
        public int numItem { get; set; }

        [Precision(11, 8)]
        public decimal cantidad { get; set; }
        public string codigo { get; set; }
        public short uniMedida { get; set; }
        public string descripcion { get; set; }

        [Precision(11, 8)]
        public decimal precioUni { get; set; }

        [Precision(11, 8)]
        public decimal montoDescu { get; set; }

        [Precision(11, 8)]
        public decimal ventaGravada { get; set; }
        public List<string> tributos { get; set; } = new List<string>();

        [Precision(11, 8)]
        public decimal noGravado { get; set; }
    }

    public class FeFexV1OthersDocumentsDTO
    {
        public int codDocAsociado { get; set; }
        public string descDocumento { get; set; }
        public string detalleDocumento { get; set; }
        public string placaTrans { get; set; }
        public int? modoTransp { get; set; }
        public string numConductor { get; set; }
        public string nombreConductor { get; set; }
    }

    public class FeFexV1ReceiverDTO
    {
        public string tipoDocumento { get; set; }
        public string numDocumento { get; set; }
        public string nombre { get; set; }
        public string nombreComercial { get; set; } = null;
        public string codPais { get; set; }
        public string nombrePais { get; set; }
        public string complemento { get; set; }
        public short tipoPersona { get; set; }
        public string descActividad { get; set; }
        public string telefono { get; set; }
        public string correo { get; set; }
    }

    public class FeFexV1EmisorDTO
    {
        public string nit { get; set; }
        public string nrc { get; set; }
        public string nombre { get; set; }
        public string codActividad { get; set; }
        public string descActividad { get; set; }
        public string nombreComercial { get; set; } = null;
        public string tipoEstablecimiento { get; set; }
        public FeAddressDTO direccion { get; set; } = new FeAddressDTO();
        public string telefono { get; set; }
        public string correo { get; set; }
        public string codEstableMH { get; set; } = null;
        public string codEstable { get; set; } = null;
        public string codPuntoVentaMH { get; set; } = null;
        public string codPuntoVenta { get; set; } = null;
        public int tipoItemExpor { get; set; }
        public string recintoFiscal { get; set; } = null;
        public string regimen { get; set; } = null;
    }

    public class FeFexV1IdentificacionDTO
    {
        public short version { get; set; }
        public string ambiente { get; set; }
        public string tipoDte { get; set; }
        public string numeroControl { get; set; }
        public string codigoGeneracion { get; set; }
        public int tipoModelo { get; set; }
        public int tipoOperacion { get; set; }
        public string tipoContingencia { get; set; } = null;
        public string motivoContigencia { get; set; } = null;
        public string fecEmi { get; set; }
        public string horEmi { get; set; }
        public string tipoMoneda { get; set; }
    }
}
