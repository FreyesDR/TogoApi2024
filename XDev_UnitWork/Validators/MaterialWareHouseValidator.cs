using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Material;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Validators
{
    public class MaterialWareHouseValidator : AbstractValidator<MaterialWareHouseDTO>
    {
        public MaterialWareHouseValidator(IWareHouseBL wareHouseBL, IBranchBL branchBL, IMaterialBL materialBL)
        {
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

            RuleFor(x => x.WareHouseId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                      .MustAsync(async (whid, _) =>
                                      {
                                          return await wareHouseBL.AnyAsync(whid);
                                      }).WithMessage("Almacén no existe");
        }
    }
}
