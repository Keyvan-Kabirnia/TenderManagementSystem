using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Domain.Entities;

namespace Tms.Domain.Interfaces;
public interface IVendorRepository
{
    Task<VendorEntity?> GetByIdAsync(int id);

    Task<IEnumerable<VendorEntity>> GetAllAsync();

    Task<VendorEntity> AddAsync(VendorEntity entity);

    Task UpdateAsync(VendorEntity entity);

    Task DeleteAsync(VendorEntity entity);

    Task<VendorEntity?> GetVendorWithBidsAsync(int id);

    Task<IEnumerable<VendorEntity>> GetVendorsWithBidSummaryAsync(int page, int pageSize);

    Task<int> GetTotalCountAsync();
}
