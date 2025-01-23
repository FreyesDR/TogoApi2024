using FluentValidation;
using XDev_UnitWork.Business;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.DM;

namespace XDev_UnitWork.Validators
{
    public class CurrencyValidator: AbstractValidator<CurrencyDTO>
    {
        public CurrencyValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(3).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.ISOCode).MaximumLength(3).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(50).WithMessage(UtilsExtension.fieldMaxLength);
        }
    }
}
