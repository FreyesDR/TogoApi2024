using System.ComponentModel.DataAnnotations;

namespace XDev_UnitWork.DTO.Dte
{
	public class JsonContingencyV3
	{
		public JsonIdentContingencyV3 identificacion { get; set; } = new JsonIdentContingencyV3();
		public JsonEmisorContingencyV3 emisor { get; set; } = new JsonEmisorContingencyV3();
		public List<JsonDetailContingencyV3> detalleDTE { get; set; } = new List<JsonDetailContingencyV3>();
		public JsonReasonContingencyV3 motivo { get; set; } = new JsonReasonContingencyV3();
	}

	public class JsonReasonContingencyV3
	{
		public string fInicio { get; set; }
		public string fFin { get; set; }
		public string hInicio { get; set; }
		public string hFin { get; set; }
		public int tipoContingencia { get; set; }
		public string motivoContingencia { get; set; }
	}

	public class JsonDetailContingencyV3
	{
		public int noItem { get; set; } = 1;
		public string codigoGeneracion { get; set; }
		public string tipoDoc { get; set; } = null;
	}

	public class JsonEmisorContingencyV3
	{
		[MaxLength(14)]
		public string nit { get; set; }
		public string nombre { get; set; }
		public string nombreResponsable { get; set; }
		public string tipoDocResponsable { get; set; }
		public string numeroDocResponsable { get; set; } = null;
		public string tipoEstablecimiento { get; set; }
		public string codEstableMH { get; set; } = null;
		public string codPuntoVenta { get; set; } = null;
		public string telefono { get; set; }
		public string correo { get; set; }
	}

	public class JsonIdentContingencyV3
	{
		public short version { get; set; } = 3;
		public string ambiente { get; set; }
		public string codigoGeneracion { get; set; }
		public string fTransmision { get; set; }
		public string hTransmision { get; set; }
	}
}
