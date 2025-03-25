using Microsoft.EntityFrameworkCore;

namespace XDev_UnitWork.DTO.Dte
{
    public class JsonFeCcfV3
    {
        public FeIdentificacionDTO identificacion { get; set; } = new FeIdentificacionDTO();
        public List<FeRelatedDocumentDTO> documentoRelacionado { get; set; } = null;
        public FeEmisorDTO emisor { get; set; } = new FeEmisorDTO();
        public FeCcfV3ReceptorDTO receptor { get; set; } = new FeCcfV3ReceptorDTO();
        public List<FeOtrosDocumentosDTO> otrosDocumentos { get; set; } = null;
        public FeVentaTerceroDTO ventaTercero { get; set; } = null;
        public List<FeCcfV3DetailDTO> cuerpoDocumento { get; set; } = new List<FeCcfV3DetailDTO>();
        public FeCcfV3SummaryDTO resumen { get; set; } = new FeCcfV3SummaryDTO();
        public FeExtensionDTO extension { get; set; }  = new FeExtensionDTO();
        public List<FeAppendixDTO> apendice { get; set; } = null;
    }

    public class FeCcfV3SummaryDTO
    {
        [Precision(11, 2)]
        public decimal totalNoSuj { get; set; }

        [Precision(11, 2)]
        public decimal totalExenta { get; set; }

        [Precision(11, 2)]
        public decimal totalGravada { get; set; }

        [Precision(11, 2)]
        public decimal subTotalVentas => totalNoSuj + totalExenta + totalGravada;

        [Precision(11, 2)]
        public decimal descuNoSuj { get; set; }

        [Precision(11, 2)]
        public decimal descuExenta { get; set; }

        [Precision(11, 2)]
        public decimal descuGravada { get; set; }

        [Precision(11, 2)]
        public decimal porcentajeDescuento { get; set; }

        [Precision(11, 2)]
        public decimal totalDescu { get; set; }

        public List<FeTaxesDTO> tributos { get; set; } = new List<FeTaxesDTO>();

        [Precision(11, 2)]
        public decimal subTotal { get; set; }

        [Precision(11, 2)]
        public decimal ivaPerci1 { get; set; }

        [Precision(11, 2)]
        public decimal ivaRete1 { get; set; }

        [Precision(11, 2)]
        public decimal reteRenta { get; set; }

        [Precision(11, 2)]
        public decimal montoTotalOperacion { get; set; }

        [Precision(11, 2)]
        public decimal totalNoGravado { get; set; }

        [Precision(11, 2)]
        public decimal totalPagar => montoTotalOperacion + ivaPerci1 - Math.Abs(ivaRete1) - Math.Abs(reteRenta) + totalNoGravado;

        public string totalLetras { get; set; }

        [Precision(11, 2)]
        public decimal saldoFavor { get; set; }
        public int condicionOperacion { get; set; }
        public List<FePagosDTO> pagos { get; set; } = new List<FePagosDTO>();
        public string numPagoElectronico { get; set; } = null;
    }

    public class FeCcfV3DetailDTO
    {
        public int numItem { get; set; }
        public int tipoItem { get; set; }

        public string numeroDocumento { get; set; } = null;

        [Precision(11, 8)]
        public decimal cantidad { get; set; }
        public string codigo { get; set; }
        public short uniMedida { get; set; }
        public string descripcion { get; set; }

        [Precision(11, 8)]
        public decimal precioUni { get; set; }

        [Precision(11, 8)]
        public decimal montoDescu { get; set; }
        public string codTributo { get; set; } = null;

        [Precision(11, 8)]
        public decimal ventaNoSuj { get; set; }

        [Precision(11, 8)]
        public decimal ventaExenta { get; set; }

        [Precision(11, 8)]
        public decimal ventaGravada { get; set; }
        public List<string> tributos { get; set; }

        [Precision(11, 8)]
        public decimal psv { get; set; }

        [Precision(11, 8)]
        public decimal noGravado { get; set; }
    }

    public class FeCcfV3ReceptorDTO
    {
        public string nit { get; set; }
        public string nrc { get; set; }
        public string nombre { get; set; }
        public string codActividad { get; set; }
        public string descActividad { get; set; }
        public string nombreComercial { get; set; } = null;
        public FeAddressDTO direccion { get; set; } = new FeAddressDTO();
        public string telefono { get; set; }
        public string correo { get; set; }
    }
}
