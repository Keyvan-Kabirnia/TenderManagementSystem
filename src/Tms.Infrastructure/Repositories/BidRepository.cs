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
public class BidRepository(TmsDbContext dbContext) : IBidRepository
{
    private readonly DbSet<BidEntity> _dbset = dbContext.Set<BidEntity>();

    public virtual async Task<BidEntity?> GetByIdAsync(int id)
    {
        return await _dbset.FindAsync(id);
    }

    public virtual async Task<IEnumerable<BidEntity>> GetAllAsync()
    {
        return await _dbset.ToListAsync();
    }

    public virtual async Task<BidEntity> AddAsync(BidEntity entity)
    {
        await _dbset.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(BidEntity entity)
    {
        _dbset.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(BidEntity entity)
    {
        _dbset.Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}
