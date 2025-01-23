using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Company;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Validators
{
    public class CompanyIDValidator : AbstractValidator<CompanyIDDTO>
    {
        public CompanyIDValidator(ICompanyBL companyBL, IIDTypeBL iDTypeBL)
        {
            RuleFor(x => x.DocumentNumber).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                          .MaximumLength(30).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.CompanyId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                     .MustAsync(async (companyid, _) =>
                                     {
                                         return await companyBL.AnyAsync(companyid);
                                     }).WithMessage("Compañia incorrecta");

            RuleFor(x => x.IDTypeId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                     .MustAsync(async (idtype, _) =>
                                     {
                                         return await iDTypeBL.AnyAsync(idtype);
                                     }).WithMessage("Tipo identificación incorrecta");
        }
    }
}
