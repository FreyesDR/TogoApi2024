using System.ComponentModel;

namespace XDev_UnitWork.DTO.SaleOrder
{
    public class SaleOrderPdfFormDTO
    {
        public Byte[] QrCode { get; set; }
        public Byte[] CompanyLogo { get; set; }
        [DisplayName("Fecha")]
        public DateTime SODate { get; set; } // Fecha del pedido        
        public string SONumber { get; set; }
        public string SOType { get; set; }

        public string PartnerNum { get; set; }
        public string PartnerName { get; set; }
        public string PartnerAddress { get; set; }
        public string PartnerCityName { get; set; }
        public string PartnerRegionName { get; set; }
        public string PartnerCountryName { get; set; }
        public string PartnerNIT { get; set; }
        public string PartnerNRC { get; set; }
        public string PartnerActivity { get; set; }
        public string PartnerEmail { get; set; }
        public string PartnerPhone { get; set; }
        public string PartnerTypeDocument { get; set; }

        public string CompanyNum { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyCityName { get; set; }
        public string CompanyRegionName { get; set; }
        public string CompanyCountryName { get; set; }
        public string CompanyNIT { get; set; }
        public string CompanyNRC { get; set; }
        public string CompanyActivity { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhone { get; set; }


        public string BranchNum { get; set; }
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
        public string BranchCityName { get; set; }
        public string BranchRegionName { get; set; }
        public string BranchCountryName { get; set; }
        public string BranchNIT { get; set; }
        public string BranchNRC { get; set; }
        public string BranchActivity { get; set; }
        public string BranchEmail { get; set; }
        public string BranchPhone { get; set; }
        public string PointSale { get; set; }

        public string CurrencyCode { get; set; } // Código moneda

        [DisplayName("Impuesto")]
        public decimal TaxAmount { get; set; } // Impuesto
        public decimal TaxRate { get; set; }

        [DisplayName("Descuento")]
        public decimal Discount { get; set; } // Descuento

        public decimal NoSujeto { get; set; } // Venta no Sujeta                                              
        public decimal NoGravado { get; set; } // Venta no gravada
        public decimal Exempt { get; set; } // Venta Exenta
        public decimal Gravado { get; set; } // Venta Gravada

        public decimal DescuntoGravado { get; set; }
        public decimal DescuntoExento { get; set; }
        public decimal DescuntoNoSujeto { get; set; }

        public decimal SubTotal => SumOper - Math.Abs(DescuntoGravado) - Math.Abs(DescuntoExento) - Math.Abs(DescuntoNoSujeto) + TaxAmount;
        
        public decimal SumOper => NoSujeto + Exempt + Gravado;
        public decimal TotalOper => SubTotal + Percepcion1 - Math.Abs(Retencion1) - Math.Abs(Retencion10);
        public decimal TotalPagar => TotalOper + NoGravado;

        public decimal NetAmount { get; set; } // Total
        public string TotalLetras { get; set; }

        public decimal Retencion1 { get; set; }
        public decimal Retencion10 { get; set; }
        public decimal Percepcion1 { get; set; }
        public decimal Seguro { get; set; }
        public decimal Flete { get; set; }

        public string CustomReference { get; set; } // Referencia del cliente
        public DateTime CustomReferenceDate { get; set; } // Fecha referencia cliente        
        public string PaymentCondition { get; set; }

        public string IncoTerms { get; set; }
    }
}
