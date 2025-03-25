namespace XDev_UnitWork.DTO.FeSv
{
    public class FeCancelDTO
    {
        public string CancelType { get; set; } = "1";
        public Guid InvoiceId { get; set; }
        public string CodGenR { get; set; } = string.Empty;
        public Guid IDTypeId { get; set; }
        public string TipoDoc { get; set; } = string.Empty ;
        public string NumDoc { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Motivo { get; set; } = string.Empty;
        public string Solicita { get; set; } = string.Empty;
        public string SolicitaTipoDoc { get; set; } = string.Empty;
        public string SolicitaNumDoc {  get; set; }= string.Empty;

        //public static ValueTask<FeCancelDTO> BindAsync(HttpContext context)
        //{            

        //    var result = new FeCancelDTO
        //    {
        //        CancelType = context.GetValueOrDefault(nameof(CancelType), string.Empty),
        //        InvoiceId = context.GetValueOrDefault(nameof(InvoiceId),Guid.Empty),
        //        CodGenR = context.GetValueOrDefault(nameof(CodGenR),string.Empty),
        //        TipoDoc = context.GetValueOrDefault(nameof(TipoDoc), string.Empty),
        //        NumDoc = context.GetValueOrDefault(nameof(NumDoc), string.Empty),
        //        Nombre = context.GetValueOrDefault(nameof(Nombre), string.Empty),
        //        Phone = context.GetValueOrDefault(nameof(Phone), string.Empty),
        //        Email = context.GetValueOrDefault(nameof(Email), string.Empty),
        //        Motivo = context.GetValueOrDefault(nameof(Motivo), string.Empty),
        //        Solicita = context.GetValueOrDefault(nameof(Solicita), string.Empty),
        //        SolicitaNumDoc = context.GetValueOrDefault(nameof(SolicitaNumDoc), string.Empty),
        //        SolicitaTipoDoc = context.GetValueOrDefault(nameof(SolicitaTipoDoc), string.Empty),
        //    };

        //    return ValueTask.FromResult(result);
        //}
    }
}
