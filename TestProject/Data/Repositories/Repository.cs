using Microsoft.EntityFrameworkCore;
using TestProject.Data.Contexts;
using TestProject.Data.Models.Commons;

namespace TestProject.Data.Repositories;

public class Repository<T, TId> : IRepository<T, TId>
    where TId : struct
    where T : Auditable<TId>
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T> CreateAsync(T entity)
    {
        try
        {
            entity.CreatedAt = DateTime.UtcNow;
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<bool> DeleteAsync(TId id)
    {
        try
        {
            var result = await _dbSet.FirstOrDefaultAsync(e => e.Id.Equals(id) && !e.IsDeleted);
            result.IsDeleted = true;
            result.DeletedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public IQueryable<T> GetAll()
    {
            var result = _context.Set<T>().Where(x => x.IsDeleted == false).AsQueryable();
            return result;
    }

    public async Task<T> GetByIdAsync(TId id)
    {
            var result = await _dbSet.FirstOrDefaultAsync(e => e.Id.Equals(id) && !e.IsDeleted);
            return result;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        try
        {
            entity.UpdatedAt = DateTime.UtcNow;
            var result = _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
