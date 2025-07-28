using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Domain.Entities;

namespace Tms.Domain.Interfaces;
public interface IStatusRepository
{
    Task<StatusEntity?> GetByIdAsync(int id);

    Task<IEnumerable<StatusEntity>> GetAllAsync();

    Task<StatusEntity> AddAsync(StatusEntity entity);

    Task UpdateAsync(StatusEntity entity);

    Task DeleteAsync(StatusEntity entity);
}
