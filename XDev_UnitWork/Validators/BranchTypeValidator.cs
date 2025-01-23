using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Company;

namespace XDev_UnitWork.Validators
{
    public class BranchTypeValidator : AbstractValidator<BranchTypeDTO>
    {
        public BranchTypeValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(2).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(30).WithMessage(UtilsExtension.fieldMaxLength);
        }
    }
}
