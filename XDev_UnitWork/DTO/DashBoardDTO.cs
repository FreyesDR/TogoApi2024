using Microsoft.EntityFrameworkCore;

namespace XDev_UnitWork.DTO
{
    public class DashBoardDTO
    {
        public int SaleOrders {  get; set; }
        public int Invoices { get; set; }
        public int InvoicesCanceled { get; set; }
        public int InvoicesContingency { get; set; }
    }

    public class DashBoardSaleDTO
    {
        public List<DbSaleDTO> Ventas { get; set; }
        public List<DbSaleDTO> Devoluciones { get; set; }
        public List<DbSaleDocDTO> Docs { get; set; }
        public List<DbSalePartnerDTO> Partners { get; set; }
        public List<DbSaleTipoMPDTO> Tipo { get; set; }
        public List<DbSaleMPDTO> MeansPayment { get; set; }
    }

    public class DbSalesMPDTO
    {
        public string Tipo { get; set; }
        public string Name { get; set; }
        [Precision(18, 2)]
        public decimal Amount { get; set; }
    }

    public class DbSalePartnerDTO
    {
        [Precision(18, 2)]
        public decimal Value { get; set; }
        public string Name { get; set; }
    }

    public class DbSaleDTO
    {
        public DateTime Fecha { get; set; }
        [Precision(18, 2)]
        public decimal Total { get; set; }        
    }

    public class DbSaleDocDTO
    {        
        [Precision(18, 2)]
        public decimal Value { get; set; }
        public string Name { get; set; }
    }

    public class DbSaleTipoMPDTO
    {
        [Precision(18, 2)]
        public decimal Value { get; set; }
        public string Name { get; set; }
    }

    public class DbSaleMPDTO
    {
        [Precision(18, 2)]
        public decimal Value { get; set; }
        public string Name { get; set; }
    }
}
