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
public class VendorRepository(TmsDbContext dbContext) : IVendorRepository
{
    private readonly DbSet<VendorEntity> _dbset = dbContext.Set<VendorEntity>();

    public async Task<VendorEntity> AddAsync(VendorEntity entity)
    {
        await _dbset.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task UpdateAsync(VendorEntity entity)
    {
        _dbset.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(VendorEntity entity)
    {
        _dbset.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    // should apply dapper
    public async Task<VendorEntity?> GetByIdAsync(int id)
    {
        return await _dbset.FindAsync(id);
    }

    public async Task<IEnumerable<VendorEntity>> GetAllAsync()
    {
        return await _dbset.ToListAsync();
    }

    public async Task<VendorEntity?> GetVendorWithBidsAsync(int id)
    {
        return await _dbset
            .AsNoTracking()
            .Include(v => v.User)
            .Include(v => v.Bids)
            .ThenInclude(b => b.Tender)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<IEnumerable<VendorEntity>> GetVendorsWithBidSummaryAsync(int page, int pageSize)
    {
        return await _dbset
            .AsNoTracking()
            .Include(v=>v.User)
            .Include(v => v.Bids)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> GetTotalCountAsync()
    {
        return await _dbset.CountAsync();
    }
}
