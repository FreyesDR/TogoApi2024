using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Validators
{
    public class CityValidator : AbstractValidator<CityDTO>
    {
        public CityValidator(IRegionBL regionBL)
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(4).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.RegionId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                     .MustAsync(async (regionid, _) =>
                                     {
                                         return await regionBL.AnyAsync(regionid);
                                     }).WithMessage("Error determinado región");


            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(50).WithMessage(UtilsExtension.fieldMaxLength);
        }
    }
}
