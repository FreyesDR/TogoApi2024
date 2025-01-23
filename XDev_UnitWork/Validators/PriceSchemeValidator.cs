using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.PriceScheme;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Validators
{
    public class PriceSchemeValidator: AbstractValidator<PriceSchemeDTO>
    {
        public PriceSchemeValidator(IPriceConditionBL priceConditionBL)
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(5).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(50).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Conditions)
                .Must(x => x is not null && x.Count > 0).WithMessage("Debe ingresar al menos una condición")
                .ForEach(rule =>
                {
                    rule.MustAsync(async (condition, _) =>
                    {
                        return await priceConditionBL.AnyAsync(condition.PriceConditionId);
                    }).WithMessage("Condición no existe");
                });
                
        }
    }
}
