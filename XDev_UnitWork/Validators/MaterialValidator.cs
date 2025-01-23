using FluentValidation;
using XDev_Model.Entities;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Material;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Validators
{
    public class MaterialValidator : AbstractValidator<MaterialDTO>
    {
        public MaterialValidator(IMaterialTypeBL materialType)
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(20).WithMessage(UtilsExtension.fieldMaxLength)
                                .When(f => f.NumType == 0,ApplyConditionTo.AllValidators);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(100).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.MaterialTypeId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                           .MustAsync(async (typeid, _) =>
                                           {
                                               return await materialType.AnyAsync(typeid);
                                           }).WithMessage("Tipo material no existe");

            RuleFor(x => x.OldCode).MaximumLength(20).WithMessage(UtilsExtension.fieldMaxLength);
        }
    }
}
