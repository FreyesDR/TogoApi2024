namespace XDev_UnitWork.DTO.Invoice
{
    public class InvoiceFormDTO
    {
        public Byte[] QrCode { get; set; }
        public Byte[] CompanyLogo { get; set; }
        public DateTime SODate { get; set; }
        public string SONumber { get; set; }
        public string SOType { get; set; }
        public string CodGen { get; set; }
        public string NumControl { get; set; }
        public string SelloRecepcion { get; set; }
        public DateTime FechaRecepcion { get; set; }
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
        public string PartnerPayCondition { get; set; }

        public string CompanyName { get; set; }
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
        public string BranchEmail { get; set; }
        public string BranchPhone { get; set; }
        public string PointSale { get; set; }

        public string CurrencyCode { get; set; } // Código moneda        
        public decimal TaxAmount { get; set; } // Impuesto
        public string TaxRate { get; set; }
        public decimal Discount { get; set; } // Descuento
        public decimal NoSujeto { get; set; } // Venta no Sujeta                                              
        public decimal NoGravado { get; set; } // Venta no gravada
        public decimal Exempt { get; set; } // Venta Exenta
        public decimal Gravado { get; set; } // Venta Gravada
        public decimal DescuentoNoSujeto { get; set; }
        public decimal DescuentoExento { get; set; }
        public decimal DescuentoGravado { get; set; }
        public decimal SumOper => NoSujeto + Exempt + Gravado;
        public decimal SubTotal => SumOper - Math.Abs(DescuentoNoSujeto) - Math.Abs(DescuentoExento) - Math.Abs(DescuentoGravado) + TaxAmount;        
        public decimal TotalOper => SubTotal + Percepcion1 - Math.Abs(Retencion1) - Math.Abs(Retencion10);
        public decimal TotalPagar => TotalOper + NoGravado;
        public string TotalLetras { get; set; }
        public decimal Retencion1 { get; set; }
        public decimal Retencion10 { get; set; }
        public decimal Percepcion1 { get; set; }
        public decimal Seguro { get; set; }
        public decimal Flete { get; set; }
        public string IncoTerms { get; set; }

        public string PersonReceiver { get; set; }
        public string PersonSender { get; set; }
        public string PersonReceiverNumDoc { get; set; }
        public string PersonSenderNumDocNum { get; set; }
        public string Recinto { get; set; }
        public string Regimen { get; set; }
        public string Observaciones { get; set; }
    }

    internal class InvoiceFormPositionDTO
    {
        public int Position { get; set; }
        public string MaterialTypeCode { get; set; }
        public string MaterialCode { get; set; }
        public string MaterialName { get; set; }
        public string PriceType { get; set; } // GV = Gravado, EX = Exento, NS = No Sujeto, NG = No Gravado        
        public string UnitMeasureCode { get; set; }
        public decimal Quantity { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Gravado { get; set; }
        public decimal Exento { get; set; }
        public decimal NoSujeto { get; set; }
        public decimal NoGravado { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class InvoiceFormDocumentsDTO
    {
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public DateTime DocumentDate { get; set; }
    }
}
