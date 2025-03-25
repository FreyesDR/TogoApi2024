using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Admin;

namespace XDev_UnitWork.Validators
{
    public class UserValidator: AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(UtilsExtension.propertyRequired)
                                 .MaximumLength(256).WithMessage(UtilsExtension.fieldMaxLength)
                                 .EmailAddress().WithMessage(UtilsExtension.fieldEmail);            

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.propertyRequired)
                                .MaximumLength(100).WithMessage(UtilsExtension.fieldMaxLength)
                                .MinimumLength(5).WithMessage(UtilsExtension.fieldMinLength);

            RuleFor(x => x.IDNumber).MaximumLength(30).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.PhoneNumber).MaximumLength(50).WithMessage(UtilsExtension.fieldMaxLength);
        }
    }
}
