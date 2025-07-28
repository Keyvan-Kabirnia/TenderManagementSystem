using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Domain.Entities;

namespace Tms.Domain.Interfaces;
public interface IUserRepository
{
    Task<UserEntity?> GetByIdAsync(int id);

    Task<IEnumerable<UserEntity>> GetAllAsync();

    Task<UserEntity> AddAsync(UserEntity entity);

    Task UpdateAsync(UserEntity entity);

    Task DeleteAsync(UserEntity entity);
}
