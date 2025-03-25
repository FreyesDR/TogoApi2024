using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.ElectronicBilling;

namespace XDev_UnitWork.Validators
{
    public class EBillingTaxValidator: AbstractValidator<EBillingTaxDTO>
    {
        public EBillingTaxValidator()
        {
            RuleFor(x => x.TaxCode).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                   .MaximumLength(4).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.TaxName).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                   .MaximumLength(100).WithMessage(UtilsExtension.fieldMaxLength);
        }
    }
}
