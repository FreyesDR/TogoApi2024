using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;

namespace XDev_UnitWork.Validators
{
    public class ChangePasswordValidator: AbstractValidator<ChangePasswordDTO>
    {
        public ChangePasswordValidator()
        {            
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage(UtilsExtension.fieldRequired);
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage(UtilsExtension.fieldRequired);
        }
    }
}
