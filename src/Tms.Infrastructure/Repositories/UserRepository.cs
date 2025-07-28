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
public class UserRepository(TmsDbContext dbContext) : IUserRepository
{
    private readonly DbSet<UserEntity> _dbset = dbContext.Set<UserEntity>();

    public virtual async Task<UserEntity?> GetByIdAsync(int id)
    {
        return await _dbset.FindAsync(id);
    }

    public virtual async Task<IEnumerable<UserEntity>> GetAllAsync()
    {
        return await _dbset.ToListAsync();
    }

    public virtual async Task<UserEntity> AddAsync(UserEntity entity)
    {
        await _dbset.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public virtual async Task UpdateAsync(UserEntity entity)
    {
        _dbset.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public virtual async Task DeleteAsync(UserEntity entity)
    {
        _dbset.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    // should apply dapper
    public virtual async Task<UserEntity?> GetByUserNameAsync(string userName)
    {
        return await _dbset.AsNoTracking().FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public virtual async Task<bool> UserNameExistsAsync(string userName)
    {
        return await _dbset.AnyAsync(u => u.UserName == userName);
    }

    public virtual async Task<bool> EmailExistsAsync(string email)
    {
        return await _dbset.AnyAsync(u => u.Email == email);
    }
}
