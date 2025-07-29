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
public class TenderRepository(TmsDbContext dbContext) : ITenderRepository
{
    private readonly DbSet<TenderEntity> _dbset = dbContext.Set<TenderEntity>();

    public virtual async Task<TenderEntity> AddAsync(TenderEntity entity)
    {
        await _dbset.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(TenderEntity entity)
    {
        _dbset.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(TenderEntity entity)
    {
        _dbset.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    // should apply dapper
    public virtual async Task<TenderEntity?> GetByIdAsync(int id)
    {
        return await _dbset.FindAsync(id);
    }

    public virtual async Task<IEnumerable<TenderEntity>> GetAllAsync()
    {
        return await _dbset.ToListAsync();
    }

    public virtual async Task<TenderEntity?> GetTenderWithDetailsAsync(int id)
    {
        return await _dbset
            .AsNoTracking()
            .Include(t => t.Owner)
            .Include(t => t.Category)
            .Include(t => t.Status)
            .Include(t => t.Bids)
                .ThenInclude(b => b.Vendor)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public virtual async Task<IEnumerable<TenderEntity>> GetTendersWithCategoryAndStatusAsync(int page, int pageSize)
    {
        return await _dbset
            .AsNoTracking()
            .Include(t => t.Category)
            .Include(t => t.Status)
            .OrderByDescending(t => t.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public virtual async Task<int> GetTotalCountAsync()
    {
        return await _dbset.CountAsync();
    }
}
