using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;

namespace XDev_UnitWork.Validators
{
    public class PartnerTypeValidator: AbstractValidator<PartnerTypeDTO>
    {
        public PartnerTypeValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(2).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(25).WithMessage(UtilsExtension.fieldMaxLength);
        }
    }
}
