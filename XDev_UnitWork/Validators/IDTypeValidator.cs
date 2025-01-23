using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.DM;

namespace XDev_UnitWork.Validators
{
    public class IDTypeValidator : AbstractValidator<IDTypeDTO>
    {
        public IDTypeValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(5).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Code).MaximumLength(3).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(50).WithMessage(UtilsExtension.fieldMaxLength);
        }
    }
}
