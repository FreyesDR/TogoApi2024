using AutoMapper;
using System.Reflection;
using XDev_Model;

namespace XDev_UnitWork.Business
{
    public class GenericBL<T>
    {
        public GenericBL(ApplicationDbContext dbContext,
                         IHttpContextAccessor contextAccessor,
                         IMapper mapper)
        {
            DbContext = dbContext;
            ContextAccessor = contextAccessor;
            Mapper = mapper;
            Repository = CreateInstance();
        }

        private T CreateInstance()
        {
            Type[] iLoadTypes = (from t in Assembly.Load("XDev_Model").GetExportedTypes()
                                 where !t.IsInterface && !t.IsAbstract
                                 where typeof(T).IsAssignableFrom(t)
                                 select t).ToArray();

            var iload = iLoadTypes.FirstOrDefault();
            var tipo = (T)Activator.CreateInstance(iload, DbContext);
            return tipo;
        }

        public ApplicationDbContext DbContext { get; }
        public IHttpContextAccessor ContextAccessor { get; }
        public IMapper Mapper { get; }
        public T Repository { get; internal set; }
    }
}
