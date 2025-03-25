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

            RuleFor(x => x.ApiUser).MaximumLength(20).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.ApiKeyTest).MaximumLength(50).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.ApiKeyProd).MaximumLength(50).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.PrivateKeyTest).MaximumLength(50).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.PrivateKeyProd).MaximumLength(50).WithMessage(UtilsExtension.fieldMaxLength);

            When(w => w.SmtpService == "1", () => {
                RuleFor(x => x.SmtpHost).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                        .MaximumLength(30).WithMessage(UtilsExtension.fieldMaxLength);

                RuleFor(x => x.SmtpUserName).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                            .MaximumLength(100).WithMessage(UtilsExtension.fieldMaxLength);

                RuleFor(x => x.SmtpUserPassword).MaximumLength(30).WithMessage(UtilsExtension.fieldMaxLength);

                RuleFor(x => x.FromName).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                        .MaximumLength(100).WithMessage(UtilsExtension.fieldMaxLength);
            });

            
        }
    }
}
