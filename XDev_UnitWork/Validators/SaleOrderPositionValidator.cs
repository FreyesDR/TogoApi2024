using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using XDev_Model;
using XDev_UnitWork.DTO.SaleOrder;

namespace XDev_UnitWork.Validators
{
    public class SaleOrderPositionValidator: AbstractValidator<SaleOrderPositionDTO>
    {
        private ApplicationDbContext dbContext;

        public SaleOrderPositionValidator(Guid branchid, DbContextOptions<ApplicationDbContext> dbContextOptions, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {            
            dbContext = new ApplicationDbContext(dbContextOptions, httpContextAccessor, configuration);

            
        }
    }
}
