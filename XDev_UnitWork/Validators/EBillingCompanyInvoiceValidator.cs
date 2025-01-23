using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.ElectronicBilling;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Validators
{
    public class EBillingCompanyInvoiceValidator : AbstractValidator<EBillingCompanyInvoiceDTO>
    {
        public EBillingCompanyInvoiceValidator(IInvoiceTypeBL invoiceTypeBL, IEBillingDocumentBL eBillingDocumentBL)
        {
            RuleFor(x => x.InvoiceTypeId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                         .MustAsync(async (id, _) =>
                                         {
                                             return await invoiceTypeBL.AnyAsync(id);
                                         }).WithMessage("Tipo Facura no Existe");

            RuleFor(x => x.EBillingDocumentId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                              .MustAsync(async (id, _) =>
                                              {
                                                  return await eBillingDocumentBL.AnyAsync(id);
                                              }).WithMessage("Documento Electrónico no existe");

            RuleFor(x => x.RangeStart).GreaterThanOrEqualTo(0).WithMessage(UtilsExtension.fieldGreaterThanOrEqualTo)
                                    .LessThan(p => p.RangeEnd).WithMessage(UtilsExtension.fieldLessThan);

            RuleFor(x => x.RangeEnd).GreaterThan(p => p.RangeStart).WithMessage(UtilsExtension.fieldGreaterThan)
                                    .LessThanOrEqualTo(999999999999999).WithMessage(UtilsExtension.fieldLessThanOrEqualTo);

            RuleFor(x => x.Current).GreaterThanOrEqualTo(p => p.RangeStart).WithMessage(UtilsExtension.fieldGreaterThanOrEqualTo)
                                      .LessThanOrEqualTo(p => p.RangeEnd).WithMessage(UtilsExtension.fieldLessThan);

            When(w => w.ReStartYear == true, () => { 
                RuleFor(year => year.NextReStart).GreaterThan(DateTime.Now.Year).WithMessage(UtilsExtension.fieldGreaterThan);
            });
        }
    }
}
