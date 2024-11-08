using AutoMapper;
using XDev_Model;
using XDev_Model.Entities;
using XDev_Model.Interfaces;
using XDev_UnitWork.DTO;
using XDev_UnitWork.Interfaces;

namespace XDev_UnitWork.Business
{
    public class AppLogBL : IAppLogBL
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IServiceScopeFactory scopeFactory;
        private readonly IMapper mapper;

        public AppLogBL(IServiceScopeFactory scopeFactory, IMapper mapper, ApplicationDbContext dbContext) 
        {
            this.dbContext = dbContext;
            this.scopeFactory = scopeFactory;
            this.mapper = mapper;
        }

        public Task<bool> AnyAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> AnyAsync(string code)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(AppLogDTO dto)
        {
            var model = mapper.Map<AppLog>(dto);

            var scope = scopeFactory.CreateScope();
            var _dbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();

            await _dbContext.AppLog.AddAsync(model);
            await _dbContext.SaveChangesAsync();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AppLogDTO> GetByIdAsync(params object[] ids)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppLogDTO>> GetListAsync(PaginationDTO pagination)
        {
            throw new NotImplementedException();
        }

        public Task<List<AppLogDTO>> GetListAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(AppLogDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
