using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Address;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Validators
{
    public class RegionValidator : AbstractValidator<RegionDTO>
    {
        public RegionValidator(ICountryBL countryBL)
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(4).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.CountryId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                     .MustAsync(async (countryid, _) =>
                                     {
                                         return await countryBL.AnyAsync(countryid);
                                     }).WithMessage("Error determinado país");


            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(50).WithMessage(UtilsExtension.fieldMaxLength);
        }
    }
}
