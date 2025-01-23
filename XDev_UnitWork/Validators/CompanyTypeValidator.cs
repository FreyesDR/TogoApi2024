using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Company;

namespace XDev_UnitWork.Validators
{
    public class CompanyTypeValidator: AbstractValidator<CompanyTypeDTO>
    {
        public CompanyTypeValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(1).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(25).WithMessage(UtilsExtension.fieldMaxLength);
        }
    }
}
