using TestProject.Data.Models.Commons;

namespace TestProject.Data.Repositories;

    public interface IRepository<T, TId>
    where TId : struct
    where T : Auditable<TId>
{
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(TId id);
        Task<T> GetByIdAsync(TId id);
        IQueryable<T> GetAll();
    }
