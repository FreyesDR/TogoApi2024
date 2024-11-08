using FluentValidation;
using XDev_UnitWork.Business;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Validators
{
    public class BranchValidator : AbstractValidator<BranchDTO>
    {
        public BranchValidator(IBranchTypeBL branchTypeBL)
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(4).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(100).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.BranchTypeId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                        .MustAsync((branchtype, _) => {
                                            return branchTypeBL.AnyAsync(branchtype);
                                        }).WithMessage("Tipo sucursal incorrecta");

        }
    }
}
