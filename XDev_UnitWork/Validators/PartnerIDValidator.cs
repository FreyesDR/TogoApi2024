using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Partner;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Validators
{
    public class PartnerIDValidator : AbstractValidator<PartnerIDDTO>
    {
        public PartnerIDValidator(IIDTypeBL iDTypeBL, IPartnerBL partnerBL)
        {
            RuleFor(x => x.DocumentNumber).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                          .MaximumLength(30).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.PartnerId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                     .MustAsync(async (partnerid, _) =>
                                     {
                                         return await partnerBL.AnyAsync(partnerid);
                                     }).WithMessage("Socio incorrecto");

            RuleFor(x => x.IDTypeId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                     .MustAsync(async (idtype, _) =>
                                     {
                                         return await iDTypeBL.AnyAsync(idtype);
                                     }).WithMessage("Tipo identificación incorrecta");
        }
    }
}
