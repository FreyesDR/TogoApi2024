using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Company;

namespace XDev_UnitWork.Validators
{
    public class PointSaleValidator : AbstractValidator<PointSaleDTO>
    {
        public PointSaleValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(4).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(50).WithMessage(UtilsExtension.fieldMaxLength);
        }
    }
}
