using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Validators
{
    public class CompanyEconomicActivityValidator : AbstractValidator<CompanyEconomicActivityDTO>
    {
        public CompanyEconomicActivityValidator(ICompanyBL companyBL, IEconomicActivityBL economicActivityBL)
        {
            RuleFor(x => x.CompanyId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                    .MustAsync(async (companyid, _) =>
                                    {
                                        return await companyBL.AnyAsync(companyid);
                                    }).WithMessage("Sociedad incorrecta");

            RuleFor(x => x.EconomicActivityId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                    .MustAsync(async (ecoactivityid, _) =>
                                    {
                                        return await economicActivityBL.AnyAsync(ecoactivityid);
                                    }).WithMessage("Actividad económica incorrecta");
        }
    }
}
