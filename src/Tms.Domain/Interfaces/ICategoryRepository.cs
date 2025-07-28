using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Domain.Entities;

namespace Tms.Domain.Interfaces;
public interface ICategoryRepository
{
    Task<CategoryEntity?> GetByIdAsync(int id);

    Task<IEnumerable<CategoryEntity>> GetAllAsync();

    Task<CategoryEntity> AddAsync(CategoryEntity entity);

    Task UpdateAsync(CategoryEntity entity);

    Task DeleteAsync(CategoryEntity entity);
}
