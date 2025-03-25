using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.DM;

namespace XDev_UnitWork.Validators
{
    public class IncoTermsValidator: AbstractValidator<IncoTermsDTO>
    {
        public IncoTermsValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(4).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(100).WithMessage(UtilsExtension.fieldMaxLength);
        }
    }
}
