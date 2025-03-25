using FluentValidation;
using XDev_Model.Entities;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.PriceScheme;

namespace XDev_UnitWork.Validators
{
    public class PriceConditionValidator: AbstractValidator<PriceConditionDTO>
    {
        public PriceConditionValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(3).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.AltCode).MaximumLength(4).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(30).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Type).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .Length(1).WithMessage(UtilsExtension.fieldLength);
                                

            RuleFor(x => x.Source).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .Length(1).WithMessage(UtilsExtension.fieldLength);

            RuleFor(x => x.ValueType).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                     .Length(1).WithMessage(UtilsExtension.fieldLength);
        }
    }
}
