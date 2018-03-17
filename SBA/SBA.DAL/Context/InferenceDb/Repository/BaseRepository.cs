using SBA.DAL.Context.InferenceDb.Infrastructure;
using System.Linq;

namespace SBA.DAL.Context.InferenceDb.Repository
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
        private readonly SbaInferenceContext _inferenceContext;

        public BaseRepository() =>
            _inferenceContext = SbaInferenceContext.Create();

        public void Add<T>(T entity) where T : class =>
            _inferenceContext.Set<T>()
                .Add(entity);

        public void Delete<T>(T entity) where T : class =>
            _inferenceContext.Set<T>()
                .Remove(entity);

        public IQueryable<T> Queryable<T>() where T : class =>
            _inferenceContext.Set<T>()
                .AsQueryable();

        public int SaveChanges() =>
            _inferenceContext.SaveChanges();
    }
}