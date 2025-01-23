using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Material;

namespace XDev_UnitWork.Validators
{
    public class MaterialTypeValidator: AbstractValidator<MaterialTypeDTO>
    {
        public MaterialTypeValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(2).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(50).WithMessage(UtilsExtension.fieldMaxLength);
        }
    }
}
