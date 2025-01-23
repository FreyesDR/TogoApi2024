using FluentValidation;
using XDev_Model.Entities;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.SaleOrder;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Validators
{
    public class SaleOrderTypeValidator : AbstractValidator<SaleOrderTypeDTO>
    {
        public SaleOrderTypeValidator(INumberRangeBL numberRange, IInvoiceTypeBL invoiceType, IPriceSchemeBL priceSchemeBL)
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                               .MaximumLength(5).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(50).WithMessage(UtilsExtension.fieldMaxLength);



            When(inv => inv.Invoice, () =>
            {
                RuleFor(x => x.InvoiceTypeId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                            .MustAsync(async (invtype, _) =>
                                            {
                                                return await invoiceType.AnyAsync(invtype);
                                            }).WithMessage("Tipo factura no existe");
            });            

            RuleFor(x => x.RangeId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                .MustAsync(async (rangeid, _) =>
                {
                    return await numberRange.AnyAsync(rangeid);
                }).WithMessage("Rango de número no existe");

            RuleFor(x => x.PriceSchemeId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                .MustAsync(async (priceschemeid, _) => 
                { 
                    return await priceSchemeBL.AnyAsync(priceschemeid);
                }).WithMessage("Esquema de precios no existe");
        }
    }
}
