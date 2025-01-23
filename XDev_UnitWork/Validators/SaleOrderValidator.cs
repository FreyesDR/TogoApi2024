using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using XDev_Model;
using XDev_Model.Entities;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.SaleOrder;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Validators
{
    public class SaleOrderValidator : AbstractValidator<SaleOrderDTO>
    {
        protected override bool PreValidate(ValidationContext<SaleOrderDTO> context, ValidationResult result)
        {
            //RootContextData is a Dictionary<string, object> which can have any arbitrary objects added in during prevalidation
            context.RootContextData["BranchId"] = context.InstanceToValidate.BranchId;

            return base.PreValidate(context, result);
        }

        public SaleOrderValidator(ISaleOrderTypeBL saleOrderType,
                                  ICompanyBL company,
                                  IPartnerBL partnerBL,
                                  IBranchBL branchBL,
                                  ICurrencyBL currencyBL,
                                  IMaterialBL materialBL,
                                  IUnitMeasureBL unitMeasureBL,
                                  IServiceScopeFactory scopeFactory)



        {
            RuleFor(x => x.SaleOrderTypeId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                           .MustAsync(async (sotype, _) =>
                                           {
                                               return await saleOrderType.AnyAsync(sotype);
                                           }).WithMessage("Tipo pedido no existe");

            RuleFor(x => x.CompanyId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                     .MustAsync(async (coid, _) =>
                                     {
                                         return await company.AnyAsync(coid);
                                     }).WithMessage("Sociedad no existe");

            RuleFor(x => x.BranchId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                     .MustAsync(async (brid, _) =>
                                     {
                                         return await branchBL.AnyAsync(brid);
                                     }).WithMessage("Sucursal no existe");

            RuleFor(x => x.CurrencyId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                     .MustAsync(async (currid, _) =>
                                     {
                                         return await currencyBL.AnyAsync(currid);
                                     }).WithMessage("Sucursal no existe");

            When(p => p.PartnerId is not null && p.PartnerId != Guid.Empty, () =>
            {
                RuleFor(x => x.PartnerId).MustAsync(async (pid, _) =>
                {
                    return await partnerBL.AnyAsync((Guid)pid);
                }).WithMessage("Socio no existe");
            });

            RuleFor(x => x.RefDocument).MaximumLength(20).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Positions)
                .Must(x => x.Count > 0).WithMessage(UtilsExtension.fieldCount)
                .ForEach(pos =>
                {
                    pos.Custom((pos, context) =>
                    {
                        if (pos.MaterialTypeCode == "B")
                        {
                            if (pos.WareHouseId == Guid.Empty)
                            {
                                context.AddFailure(new ValidationFailure("Almacén", $"Posición {pos.Position + 1}: No se ha asignado un almacén."));
                            }
                            else
                            {
                                var scope = scopeFactory.CreateScope();
                                var _dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

                                var branchid = context.RootContextData["BranchId"].ToString().GetGuid();
                                var mwh = _dbContext.MaterialWareHouse.FirstOrDefault(f => f.MaterialId == pos.MaterialId && f.BranchId == branchid && f.WareHouseId == pos.WareHouseId);

                                if(mwh is null)
                                {
                                    context.AddFailure(new ValidationFailure("Almacén", $"Posición {pos.Position + 1}: El material no está ampliado en el almacén indicado."));
                                }
                                
                            }
                        }
                    });
                });


            //RuleFor(x => x.Positions).Custom((pos, context) => { 

            //});

            //RuleForEach(x => x.Positions).ChildRules(position =>
            //{
            //    //position.RuleFor(p => p.WareHouseId).Custom((id, context) =>
            //    //{
            //    //    var branchid = context.RootContextData["BranchId"].ToString().GetGuid();



            //    //});

            //});



            //RuleFor(x => x.Positions).Must(x => x.Count > 0).WithMessage(UtilsExtension.fieldCount)
            //                         .ForEach(pos =>
            //                         {
            //                             pos.MustAsync(async (p, _) =>
            //                             {
            //                                 return await materialBL.AnyAsync(p.MaterialId);
            //                             }).WithMessage("Material no existe");

            //                             pos.MustAsync(async (p, _) =>
            //                             {
            //                                 return await unitMeasureBL.AnyAsync(p.UnitMeasureId);
            //                             }).WithMessage("Unida de medida requerida");

            //                             pos.MustAsync(async (p, _) =>
            //                             {

            //                             }).WithMessage($"");


            //                         });
        }
    }
}
