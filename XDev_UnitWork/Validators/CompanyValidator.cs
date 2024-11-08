using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Validators
{
    public class CompanyValidator: AbstractValidator<CompanyDTO>
    {
        public CompanyValidator(ICompanyTypeBL companyTypeBL)
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(4).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(100).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.TradeName).MaximumLength(100).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.CompanyTypeId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                         .MustAsync((companytype, _) => {
                                             return companyTypeBL.AnyAsync(companytype);
                                         }).WithMessage("Tipo de sociedad incorrecta");
        }
    }
}
