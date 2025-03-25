using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using XDev_UnitWork.DTO.Address;

namespace XDev_UnitWork.DTO.Dte
{
    public class FeIdentificacionDTO
    {
        public short version { get; set; }
        public string ambiente { get; set; }
        public string tipoDte { get; set; }
        public string numeroControl { get; set; }
        public string codigoGeneracion { get; set; }
        public int tipoModelo { get; set; }
        public int tipoOperacion { get; set; }
        public int? tipoContingencia { get; set; } = null;
        public string motivoContin { get; set; } = null;
        public string fecEmi { get; set; }
        public string horEmi { get; set; }
        public string tipoMoneda { get; set; }
    }

    public class FeRelatedDocumentDTO
    {
        public string tipoDocumento { get; set; }
        public int tipoGeneracion { get; set; }
        public string numeroDocumento { get; set; }
        public string fechaEmision { get; set; }
    }

    public class FeEmisorDTO
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
        public string codEstable { get; set; } = null;
        public string codPuntoVenta { get; set; } = null;
        public string codEstableMH { get; set; }
        public string codPuntoVentaMH { get; set; }
    }

    public class FeAddressDTO
    {
        public string departamento { get; set; }
        public string municipio { get; set; }
        public string complemento { get; set; }
    }

    public class FeOtrosDocumentosDTO
    {
        public int codDocAsociado { get; set; }
        public string descDocumento { get; set; }
        public string detalleDocumento { get; set; }
        public FeMedicDTO medico { get; set; } = new FeMedicDTO();
    }

    public class FeMedicDTO
    {
        public string nombre { get; set; }
        public string nit { get; set; }
        public string docIdentificacion { get; set; }
        public int tipoServicio { get; set; }
    }

    public class FeVentaTerceroDTO
    {
        public string nit { get; set; }
        public string nombre { get; set; }
    }

    public class FeTaxesDTO
    {
        [MaxLength(2)]
        public string codigo { get; set; }
        public string descripcion { get; set; }

        [Precision(11, 2)]
        public decimal valor { get; set; }
    }

    public class FePagosDTO
    {
        public string codigo { get; set; }
        public decimal montoPago { get; set; }
        public string referencia { get; set; }
        public string? plazo { get; set; } = null;
        public int? periodo { get; set; } = null;
        
    }

    public class FeAppendixDTO
    {
        public string campo { get; set; }
        public string etiqueta { get; set; }
        public string valor { get; set; }
    }

    public class FeExtensionDTO
    {
        public string nombEntrega { get; set; }
        public string docuEntrega { get; set; }
        public string nombRecibe { get; set; }
        public string docuRecibe { get; set; }
        public string placaVehiculo { get; set; }
        public string observaciones { get; set; }
    }
}
