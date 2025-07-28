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

    public virtual async Task<VendorEntity?> GetByIdAsync(int id)
    {
        return await _dbset.FindAsync(id);
    }

    public virtual async Task<IEnumerable<VendorEntity>> GetAllAsync()
    {
        return await _dbset.ToListAsync();
    }

    public virtual async Task<VendorEntity> AddAsync(VendorEntity entity)
    {
        await _dbset.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(VendorEntity entity)
    {
        _dbset.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(VendorEntity entity)
    {
        _dbset.Remove(entity);
        await dbContext.SaveChangesAsync();
    }
}
