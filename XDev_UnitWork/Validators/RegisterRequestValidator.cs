using FluentValidation;
using Microsoft.AspNetCore.Identity.Data;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;

namespace XDev_UnitWork.Validators
{
    public class RegisterRequestValidator: AbstractValidator<RegisterDTO>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                 .EmailAddress().WithMessage(UtilsExtension.fieldEmail);

            RuleFor(x => x.Password).NotEmpty().WithMessage(UtilsExtension.fieldRequired);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(100).WithMessage(UtilsExtension.fieldMaxLength);
        }
    }
}
