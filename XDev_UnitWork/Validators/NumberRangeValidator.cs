using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.DM;

namespace XDev_UnitWork.Validators
{
    public class NumberRangeValidator : AbstractValidator<NumberRangeDTO>
    {
        public NumberRangeValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(50).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.NumStart).GreaterThanOrEqualTo(0).WithMessage(UtilsExtension.fieldGreaterThanOrEqualTo)
                                    .LessThan(p => p.NumEnd).WithMessage(UtilsExtension.fieldLessThan);

            RuleFor(x => x.NumCurrent).GreaterThanOrEqualTo(p => p.NumStart).WithMessage(UtilsExtension.fieldGreaterThanOrEqualTo)
                                      .LessThan(p => p.NumEnd).WithMessage(UtilsExtension.fieldLessThan);
        }
    }
}
