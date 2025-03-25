using Microsoft.EntityFrameworkCore;
using XDev_Model;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class DashBoardBL : IDashBoardBL
    {
        private readonly ApplicationDbContext dbContext;

        public DashBoardBL(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<DashBoardDTO> GetDashboardDataAsync()
        {
            var ret = new DashBoardDTO();

            ret.SaleOrders = await dbContext.SaleOrder.AsNoTracking().CountAsync();
            ret.Invoices = await dbContext.Invoice.AsNoTracking().CountAsync();
            ret.InvoicesCanceled = await dbContext.Invoice.AsNoTracking().CountAsync(f => f.Canceled == true);
            ret.InvoicesContingency = await dbContext.Invoice.AsNoTracking().CountAsync(f => f.Contingency == true);

            return ret;
        }

        public async Task<DashBoardSaleDTO> GetSales(int days)
        {
            return await Task.Run(async () =>
            {
                var sale = await dbContext.Database.SqlQuery<DbSaleDTO>($"select convert(date,a.Date) as Fecha, sum(a.NetAmount) + sum(a.TaxAmount) + sum(ABS(a.Per1)) - sum(ABS(a.Ret1)) - sum(ABS(a.Ret10)) as Total\r\nfrom Invoice as a inner join SaleOrder as b on a.SaleOrderId = b.Id\r\n\t\t\t      inner join SaleOrderType as c on b.SaleOrderTypeId = c.Id\r\nwhere a.Date >= convert(date, DATEADD(\"dd\",{Math.Abs(days) * -1},GETDATE())) and Canceled = 0 and c.Inventory = 'R'\r\ngroup by convert(date,a.Date), c.Inventory").ToListAsync();
                var ret = await dbContext.Database.SqlQuery<DbSaleDTO>($"select convert(date,a.Date) as Fecha, sum(a.NetAmount) + sum(a.TaxAmount) + sum(ABS(a.Per1)) - sum(ABS(a.Ret1)) - sum(ABS(a.Ret10)) as Total\r\nfrom Invoice as a inner join SaleOrder as b on a.SaleOrderId = b.Id\r\n\t\t\t      inner join SaleOrderType as c on b.SaleOrderTypeId = c.Id\r\nwhere a.Date >= convert(date, DATEADD(\"dd\",{Math.Abs(days) * -1},GETDATE())) and Canceled = 0 and c.Inventory = 'I'\r\ngroup by convert(date,a.Date), c.Inventory").ToListAsync();

                DateTime date = DateTime.Now.Date.AddDays(Math.Abs(days) * -1);
                List<DbSaleDTO> sales = new List<DbSaleDTO>();
                List<DbSaleDTO> returns = new List<DbSaleDTO>();
                while (date <= DateTime.Now.Date)
                {
                    var pos1 = new DbSaleDTO() { Fecha = date };

                    // Venta
                    var exists = sale.FirstOrDefault(f => f.Fecha == date);
                    if (exists is not null)
                        pos1.Total = exists.Total;
                    sales.Add(pos1);

                    // Devolución
                    var pos2 = new DbSaleDTO() { Fecha = date };
                    exists = ret.FirstOrDefault(f => f.Fecha == date);
                    if (exists is not null)
                        pos2.Total = exists.Total;
                    returns.Add(pos2);

                    date = date.AddDays(1);
                }

                var docs = await dbContext.Database.SqlQuery<DbSaleDocDTO>($"select b.Name, sum(a.NetAmount) + sum(a.TaxAmount) + sum(ABS(a.Per1)) - sum(ABS(a.Ret1)) - sum(ABS(a.Ret10)) as Value\r\nfrom Invoice as a inner join InvoiceType as b on b.Id = a.InvoiceTypeId\r\nwhere a.Date >= convert(date, DATEADD(\"dd\",{Math.Abs(days) * -1},GETDATE())) and Canceled = 0\r\ngroup by b.Name").ToListAsync();
                var salepartner = await dbContext.Database.SqlQuery<DbSalePartnerDTO>($"SELECT case when a.sporadic = 0 then 'Registrado' else 'Esporádico' end as Name,\r\n    sum(a.NetAmount) + sum(a.TaxAmount) + sum(ABS(a.Per1)) - sum(ABS(a.Ret1)) - sum(ABS(a.Ret10)) AS Value\r\nFROM Invoice as a inner join SaleOrder as b on a.SaleOrderId = b.Id\r\n\t\t\t      inner join SaleOrderType as c on b.SaleOrderTypeId = c.Id\r\nwhere a.Date >= convert(date, DATEADD(\"dd\",{Math.Abs(days) * -1},GETDATE())) and Canceled = 0 and c.Inventory = 'R'\r\nGROUP BY a.sporadic;").ToListAsync();

                var saleMP = await dbContext.Database.SqlQuery<DbSalesMPDTO>($"select case when d.Tipo = '1' then 'Contado' else 'Crédito' end as Tipo, e.Name, sum(d.Amount) as Amount\r\nfrom Invoice as a inner join SaleOrder as b on a.SaleOrderId = b.Id\r\n\t\t\t      inner join SaleOrderType as c on b.SaleOrderTypeId = c.Id\r\n\t\t\t\t  inner join InvoicePayment as d on d.InvoiceId = a.Id\r\n\t\t\t\t  left join MeanOfPayment as e on e.Id = d.MeanOfPaymentId\r\nwhere a.Date >= convert(date, DATEADD(\"dd\",{Math.Abs(days) * -1},GETDATE())) and Canceled = 0 and c.Inventory = 'R'\r\ngroup by d.Tipo, e.Name").ToListAsync();
                var salesMP = (from mp in saleMP
                              where mp.Tipo == "Contado"
                              group mp by new { mp.Name } into grp
                              select new DbSaleMPDTO { Name = grp.Key.Name, Value = grp.Sum(s => s.Amount) })
                              .ToList();

                var saleTipo = (from mp in saleMP                                
                                group mp by new { mp.Tipo } into grp
                                select new DbSaleTipoMPDTO { Name = grp.Key.Tipo, Value = grp.Sum(s => s.Amount) })
                                .ToList();

                var mps = await dbContext.MeanOfPayment.AsNoTracking().OrderBy(o => o.Code).ToListAsync();

                var mpList = (from mp in mps
                              join sm in salesMP on mp.Name equals sm.Name into smp
                              from xmp in smp.DefaultIfEmpty()
                              select new DbSaleMPDTO
                              {
                                  Name = mp.Name,
                                  Value = xmp == null ? 0: xmp.Value,
                              }).ToList();


                return new DashBoardSaleDTO
                {
                    Ventas = sales,
                    Devoluciones = returns,
                    Docs = docs,
                    Partners = salepartner,
                    MeansPayment = mpList,
                    Tipo = saleTipo
                };
            });
        }
    }
}
