using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.ElectronicBilling;

namespace XDev_UnitWork.Validators
{
    public class EBillingValidator: AbstractValidator<EBillingDTO>
    {
        public EBillingValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(100).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.UrlTest).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(100).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.UrlProd).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(100).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.UrlSigner).MaximumLength(100).WithMessage(UtilsExtension.fieldMaxLength);
        }
    }
}
