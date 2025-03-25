using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.DM;

namespace XDev_UnitWork.Validators
{
    public class RegimenExportValidator: AbstractValidator<RegimenExportDTO>
    {
        public RegimenExportValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(20).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(200).WithMessage(UtilsExtension.fieldMaxLength);
        }
    }
}
