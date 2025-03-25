using Microsoft.EntityFrameworkCore;

namespace XDev_UnitWork.DTO.Dte
{
    public class JsonNdV3
    {
        public FeIdentificacionDTO identificacion { get; set; } = new FeIdentificacionDTO();
        public List<FeRelatedDocumentDTO> documentoRelacionado { get; set; } = new List<FeRelatedDocumentDTO>();
        public FeNdV3TransmitterDTO emisor { get; set; } = new FeNdV3TransmitterDTO();
        public FeNdV3ReceiverDTO receptor { get; set; } = new FeNdV3ReceiverDTO();
        public FeVentaTerceroDTO ventaTercero { get; set; }
        public List<FeNdV3DetailDTO> cuerpoDocumento { get; set; } = new List<FeNdV3DetailDTO>();
        public FeNdV3SummaryDTO resumen { get; set; } = new FeNdV3SummaryDTO();
        public FeNdV3ExtensionDTO extension { get; set; } = new FeNdV3ExtensionDTO();
        public List<FeAppendixDTO> apendice { get; set; }
    }

    public class FeNdV3ExtensionDTO
    {
        public string nombEntrega { get; set; }
        public string docuEntrega { get; set; }
        public string nombRecibe { get; set; }
        public string docuRecibe { get; set; }
        public string observaciones { get; set; }
    }

    public class FeNdV3SummaryDTO
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
        public decimal totalDescu { get; set; }

        public List<FeTaxesDTO> tributos { get; set; } = new List<FeTaxesDTO>();

        [Precision(11, 2)]
        public decimal subTotal => subTotalVentas - descuNoSuj + descuExenta + descuGravada;

        [Precision(11, 2)]
        public decimal ivaPerci1 { get; set; }

        [Precision(11, 2)]
        public decimal ivaRete1 { get; set; }

        [Precision(11, 2)]
        public decimal reteRenta { get; set; }

        [Precision(11, 2)]
        public decimal montoTotalOperacion { get; set; }

        public string totalLetras { get; set; }

        public int condicionOperacion { get; set; }

        public string numPagoElectronico { get; set; } = null;
    }

    public class FeNdV3DetailDTO
    {
        public int numItem { get; set; }
        public int tipoItem { get; set; }

        public string numeroDocumento { get; set; } = null;

        [Precision(11, 8)]
        public decimal cantidad { get; set; }
        public string codigo { get; set; }
        public string codTributo { get; set; } = null;
        public short uniMedida { get; set; }
        public string descripcion { get; set; }

        [Precision(11, 8)]
        public decimal precioUni { get; set; }

        [Precision(11, 8)]
        public decimal montoDescu { get; set; }


        [Precision(11, 8)]
        public decimal ventaNoSuj { get; set; }

        [Precision(11, 8)]
        public decimal ventaExenta { get; set; }

        [Precision(11, 8)]
        public decimal ventaGravada { get; set; }
        public List<string> tributos { get; set; } = new List<string>();
    }

    public class FeNdV3ReceiverDTO
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

    public class FeNdV3TransmitterDTO
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
    }
}
