using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Material;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Validators
{
    public class MaterialBranchValidator: AbstractValidator<MaterialBranchDTO>
    {
        public MaterialBranchValidator(IBranchBL branchBL, IMaterialBL materialBL)
        {
            RuleFor(x => x.PriceSale).GreaterThanOrEqualTo(0).WithMessage(UtilsExtension.fieldGreaterThanOrEqualTo);

            RuleFor(x => x.PricePurchase).GreaterThanOrEqualTo(0).WithMessage(UtilsExtension.fieldGreaterThanOrEqualTo);

            RuleFor(x => x.MaterialId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                      .MustAsync(async (matid, _) =>
                                      {
                                          return await materialBL.AnyAsync(matid);
                                      }).WithMessage("Material no existe");

            RuleFor(x => x.BranchId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                      .MustAsync(async (brid, _) =>
                                      {
                                          return await branchBL.AnyAsync(brid);
                                      }).WithMessage("Sucursal no existe");
        }
    }
}
