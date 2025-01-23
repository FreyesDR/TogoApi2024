using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.DM;

namespace XDev_UnitWork.Validators
{
    public class EconomicActivityValidator : AbstractValidator<EconomicActivityDTO>
    {
        public EconomicActivityValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(5).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(200).WithMessage(UtilsExtension.fieldMaxLength);
        }
    }
}
