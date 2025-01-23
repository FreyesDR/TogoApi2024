using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Partner;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Validators
{
    public class PartnerValidator : AbstractValidator<PartnerDTO>
    {
        public PartnerValidator(IPartnerTypeBL partnerTypeBL)
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(10).WithMessage(UtilsExtension.fieldMaxLength)
                                .When(x => x.NumType == "0", ApplyConditionTo.AllValidators);

            RuleFor(x => x.OldCode).MaximumLength(15).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(100).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.TradeName).MaximumLength(100).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.PartnerTypeId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                         .MustAsync(async (partnertypeid, _) => {
                                             return await partnerTypeBL.AnyAsync(partnertypeid);
                                         }).WithMessage("Tipo socio incorrecto");
        }
    }
}
