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
public class StatusRepository(TmsDbContext dbContext) : IStatusRepository
{
    private readonly DbSet<StatusEntity> _dbset = dbContext.Set<StatusEntity>();

    public virtual async Task<StatusEntity> AddAsync(StatusEntity entity)
    {
        await _dbset.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(StatusEntity entity)
    {
        _dbset.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(StatusEntity entity)
    {
        _dbset.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    // should apply dapper
    public virtual async Task<StatusEntity?> GetByIdAsync(int id)
    {
        return await _dbset.FindAsync(id);
    }

    public virtual async Task<IEnumerable<StatusEntity>> GetAllAsync()
    {
        return await _dbset.ToListAsync();
    }

}
