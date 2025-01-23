using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.ElectronicBilling;

namespace XDev_UnitWork.Validators
{
    public class EBillingDocumentValidator: AbstractValidator<EBillingDocumentDTO>
    {
        public EBillingDocumentValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(4).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(50).WithMessage(UtilsExtension.fieldMaxLength);
        }
    }
}
