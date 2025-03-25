using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using XDev_Model;
using XDev_Model.Entities;
using XDev_UnitWork.Custom;
using XDev_UnitWork.DTO.SaleOrder;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Validators
{
    public class SaleOrderValidator : AbstractValidator<SaleOrderDTO>, IDisposable
    {
        private readonly ApplicationDbContext dbContext;
        private SaleOrderType saleOrderType;
        private Guid branchid;

        protected override bool PreValidate(ValidationContext<SaleOrderDTO> context, ValidationResult result)
        {
            //RootContextData is a Dictionary<string, object> which can have any arbitrary objects added in during prevalidation
            context.RootContextData["BranchId"] = context.InstanceToValidate.BranchId;
            context.RootContextData["SaleOrderTypeId"] = context.InstanceToValidate.SaleOrderTypeId;

            return base.PreValidate(context, result);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }



        //public SaleOrderValidator(ISaleOrderTypeBL saleOrderType,
        //                          ICompanyBL company,
        //                          IPartnerBL partnerBL,
        //                          IBranchBL branchBL,
        //                          ICurrencyBL currencyBL,
        //                          IMaterialBL materialBL,
        //                          IUnitMeasureBL unitMeasureBL,
        //                          IMaterialWareHouseBL materialWareHouseBL,
        //                          IInvoiceBL invoiceBL)
        public SaleOrderValidator(ApplicationDbContext _dbContext, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(_dbContext.Database.GetConnectionString()).Options;

            dbContext = new ApplicationDbContext(contextOptions, httpContextAccessor, configuration);

            RuleFor(x => x.SaleOrderTypeId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                           .MustAsync(async (sotype, _) =>
                                           {
                                               saleOrderType = await dbContext.SaleOrderType.AsNoTracking().FirstOrDefaultAsync(f => f.Id == sotype);
                                               return saleOrderType == null ? false : true;
                                           }).WithMessage("Tipo pedido no existe");

            RuleFor(x => x.CompanyId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                     .MustAsync(async (coid, _) =>
                                     {
                                         return await dbContext.Company.AsNoTracking().AnyAsync(f => f.Id == coid);
                                     }).WithMessage("Sociedad no existe");

            RuleFor(x => x.BranchId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                     .MustAsync(async (brid, _) =>
                                     {
                                         branchid = brid;
                                         return await dbContext.Branch.AsNoTracking().AnyAsync(f => f.Id == brid);
                                     }).WithMessage("Sucursal no existe");

            RuleFor(x => x.CurrencyId).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                     .MustAsync(async (currid, _) =>
                                     {
                                         return await dbContext.Currency.AsNoTracking().AnyAsync(f => f.Id == currid);
                                     }).WithMessage("Sucursal no existe");

            When(p => p.PartnerId is not null && p.PartnerId != Guid.Empty, () =>
            {
                RuleFor(x => x.PartnerId).MustAsync(async (pid, _) =>
                {
                    return await dbContext.Partner.AsNoTracking().AnyAsync(f => f.Id == (Guid)pid);
                }).WithMessage("Socio no existe");
            });

            RuleFor(x => x.Assignment).MaximumLength(50).WithMessage(UtilsExtension.fieldRequired)
                                      .MustAsync(async (assig, _) =>
                                      {
                                          if (saleOrderType is not null && saleOrderType.AssignmentRequired)
                                          {
                                              if (assig.IsNullOrEmpty())
                                                  return false;

                                              var inv = await dbContext.Invoice.AsNoTracking().FirstOrDefaultAsync(f => f.Number == assig || f.CodGeneracion == assig.ToUpper());
                                              if (inv is null)
                                                  return false;
                                          }
                                          return true;
                                      }).WithMessage("El valor de asignación es requerido y debe hacer referencia a un documento valido");


            When(p => p.Sporadic == true, () =>
            {
                RuleFor(x => x.SaleOrderSporadicPartner).NotNull().WithMessage(UtilsExtension.propertyRequired);
                RuleFor(x => x.SaleOrderSporadicPartner.Name).NotEmpty().WithMessage(UtilsExtension.fieldRequired)
                                                             .When(order => order.SaleOrderSporadicPartner is not null, ApplyConditionTo.AllValidators);
            });

            RuleFor(x => x.RefDocument).MaximumLength(20).WithMessage(UtilsExtension.fieldMaxLength);

            RuleFor(x => x.Positions).Must(x => x.Count > 0).WithMessage(UtilsExtension.fieldCount)
                                     .ForEach(pos =>
                                     {
                                         pos.MustAsync( async (p, _) => {
                                             if(p.MaterialTypeCode != "B")
                                                  return true; 

                                             var mwh = await dbContext.MaterialWareHouse.AsNoTracking().FirstOrDefaultAsync(f => f.MaterialId == p.MaterialId &&
                                                                                                                f.BranchId == branchid &&
                                                                                                                f.WareHouseId == p.WareHouseId);

                                             if(mwh is null)
                                                 return false;

                                             return true;
                                         }).WithMessage("Material no ampliado para sucursal y almacén seleccionado");
                                     });

            //RuleForEach(f => f.Positions).Where(w => w.MaterialTypeCode == "B")
            //                             .SetValidator(new SaleOrderPositionValidator(branchid, contextOptions,httpContextAccessor,configuration));


        }

        private void Validate()
        {
            

            
            //.ForEach(pos =>
            //{
            //    pos.Custom(async (pos, context) =>
            //    {
            //        if (pos.MaterialTypeCode == "B")
            //        {
            //            if (pos.WareHouseId == Guid.Empty)
            //            {
            //                context.AddFailure(new ValidationFailure("Almacén", $"Posición {pos.Position + 1}: No se ha asignado un almacén."));
            //            }
            //            else
            //            {
            //                var branchid = context.RootContextData["BranchId"].ToString().GetGuid();
            //                //var mwh = await materialWareHouseBL.GetByIdAsync(pos.MaterialId, branchid, pos.WareHouseId);
            //                var mwh = await dbContext.MaterialWareHouse.AsNoTracking().FirstOrDefaultAsync(f => f.MaterialId == pos.MaterialId &&
            //                                                                                                    f.BranchId == branchid &&
            //                                                                                                    f.WareHouseId == pos.WareHouseId);

            //                if (mwh is null)
            //                {
            //                    context.AddFailure(new ValidationFailure("Almacén", $"Posición {pos.Position + 1}: El material no está ampliado en el almacén indicado."));
            //                }

            //            }
            //        }
            //    });
            //});
        }
    }
}
