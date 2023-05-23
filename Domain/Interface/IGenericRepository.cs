using LawyerCoreApp.Domain.Models.Common;

namespace LawyerCoreApp.Domain.Interface
{

    public interface IGenericRepository<T>: IDisposable where T : BaseEntity<int>
    {
        Task Add(T entity);
        void Update(T entity);
        Task<T?> GetById(int id);
        IQueryable<T> GetAll();
        Task  Remove(int id);
        Task SaveChanges();
    }
}
