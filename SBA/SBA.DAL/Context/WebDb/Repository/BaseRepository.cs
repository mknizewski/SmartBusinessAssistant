using SBA.DAL.Context.WebDb.Infrastructure;
using System.Linq;

namespace SBA.DAL.Context.WebDb.Repository
{
    public interface IBaseRepository
    {
        IQueryable<T> Queryable<T>() where T : class;
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        int SaveChanges();
    }

    public class BaseRepository : IBaseRepository
    {
        private readonly SbaWebContext _webContext;

        public BaseRepository() =>
            _webContext = SbaWebContext.Create();

        public IQueryable<T> Queryable<T>() where T : class =>
            _webContext.Set<T>()
                .AsQueryable();

        public void Add<T>(T entity) where T : class =>
            _webContext.Set<T>()
                .Add(entity);

        public void Delete<T>(T entity) where T : class =>
            _webContext.Set<T>()
                .Remove(entity);

        public int SaveChanges() =>
            _webContext.SaveChanges();
    }
}
