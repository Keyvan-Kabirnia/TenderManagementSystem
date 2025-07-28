using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tms.Domain.Entities;
using Tms.Domain.Interfaces;
using Tms.Persistence.Data;

namespace Tms.Infrastructure.Repositories;
public class CategoryRepository(TmsDbContext dbContext) : ICategoryRepository
{
    private readonly DbSet<CategoryEntity> _dbset = dbContext.Set<CategoryEntity>();

    public virtual async Task<CategoryEntity?> GetByIdAsync(int id)
    {
        return await _dbset.FindAsync(id);
    }

    public virtual async Task<IEnumerable<CategoryEntity>> GetAllAsync()
    {
        return await _dbset.ToListAsync();
    }

    public virtual async Task<CategoryEntity> AddAsync(CategoryEntity entity)
    {
        await _dbset.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(CategoryEntity entity)
    {
        _dbset.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(CategoryEntity entity)
    {
        _dbset.Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}
