using FluentValidation;
using XDev_UnitWork.Business;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.Invoice;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Validators
{
    public class InvoiceTypeValidator: AbstractValidator<InvoiceTypeDTO>
    {
        public InvoiceTypeValidator(INumberRangeBL numberRange)
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(5).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                .MaximumLength(50).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.RangeId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                   .MustAsync(async (rangeid, _) => {
                                       return await numberRange.AnyAsync(rangeid);
                                   }).WithMessage("Rango de número no existe");
        }
    }
}
