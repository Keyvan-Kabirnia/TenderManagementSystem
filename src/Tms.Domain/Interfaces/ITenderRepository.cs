using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Domain.Entities;

namespace Tms.Domain.Interfaces;
public interface ITenderRepository
{
    Task<TenderEntity?> GetByIdAsync(int id);

    Task<IEnumerable<TenderEntity>> GetAllAsync();

    Task<TenderEntity> AddAsync(TenderEntity entity);

    Task UpdateAsync(TenderEntity entity);

    Task DeleteAsync(TenderEntity entity);
    Task<TenderEntity?> GetTenderWithDetailsAsync(int id);
    Task<IEnumerable<TenderEntity>> GetTendersWithCategoryAndStatusAsync(int page, int pageSize);
    Task<int> GetTotalCountAsync();
}
