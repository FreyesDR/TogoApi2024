using FluentValidation;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.ElectronicBilling;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Validators
{
    public class EBillingCompanyValidator : AbstractValidator<EBillingCompanyDTO>
    {
        public EBillingCompanyValidator(ICompanyBL companyBL, IAddressBL addressBL, IIDTypeBL iDTypeBL)
        {
            RuleFor(x => x.CompanyId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                     .MustAsync(async (coid, _) =>
                                     {
                                         return await companyBL.AnyAsync(coid);
                                     }).WithMessage("Sociedad no existe");

            RuleFor(x => x.AddressId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                     .MustAsync(async (addid, _) =>
                                     {
                                         return await addressBL.AnyAsync(addid);
                                     }).WithMessage("Dirección no existe");

            //When(x => x.Nif1Id != Guid.Empty, () =>
            //{
            //    RuleFor(nif => nif.Nif1Id).MustAsync(async (nifid, _) =>
            //    {
            //        return await iDTypeBL.AnyAsync(nifid);
            //    }).WithMessage("Indentificador Fiscal 1, no existe");
            //});

            //When(x => x.Nif2Id != Guid.Empty, () =>
            //{
            //    RuleFor(nif => nif.Nif2Id).MustAsync(async (nifid, _) =>
            //    {
            //        return await iDTypeBL.AnyAsync(nifid);
            //    }).WithMessage("Indentificador Fiscal 2, no existe");
            //});
        }
    }
}
