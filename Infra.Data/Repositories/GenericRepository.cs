using LawyerCoreApp.Domain.Interface;
using LawyerCoreApp.Domain.Models.Common;
using LawyerCoreApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace LawyerCoreApp.Infra.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity<int>

    {
        #region ctor
        private readonly TodoContext _context;
        private DbSet<T> dbSet;


        public GenericRepository(TodoContext context)
        {
            _context = context;
            this.dbSet = this._context.Set<T>();
        }
        #endregion

        public async Task Add(T entity)
        {
            await _context.AddAsync<T>(entity);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public IQueryable<T> GetAll()
        {
            var res = _context.Set<T>().AsQueryable<T>();
            return res;
        }

        public async Task<T?> GetById(int id)
        {
            return await dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }


        public async Task Remove(int id)
        {

            var entity = await GetById(id);
            entity.IsDeleted = true;
              Update(entity);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public   void Update(T entity)
        {
             dbSet.Update(entity);
        }
    }
}
