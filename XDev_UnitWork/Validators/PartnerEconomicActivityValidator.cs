using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Partner;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Validators
{
    public class PartnerEconomicActivityValidator : AbstractValidator<PartnerEconomicActivityDTO>
    {
        public PartnerEconomicActivityValidator(IPartnerBL partnerBL, IEconomicActivityBL economicActivityBL)
        {
            RuleFor(x => x.PartnerId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                    .MustAsync(async (partnerid, _) =>
                                    {
                                        return await partnerBL.AnyAsync(partnerid);
                                    }).WithMessage("Socio incorrecto");

            RuleFor(x => x.EconomicActivityId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                    .MustAsync(async (ecoactivityid, _) =>
                                    {
                                        return await economicActivityBL.AnyAsync(ecoactivityid);
                                    }).WithMessage("Actividad económica incorrecta");
        }
    }
}
