using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;

namespace XDev_UnitWork.Validators
{
    public class IDTypeValidator : AbstractValidator<IDTypeDTO>
    {
        public IDTypeValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(2).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(30).WithMessage(UtilsExtension.fieldMaxLength);
        }
    }
}
