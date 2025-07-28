using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tms.Domain.Entities;

namespace Tms.Domain.Interfaces;
public interface IBidRepository
{
    Task<BidEntity?> GetByIdAsync(int id);

    Task<IEnumerable<BidEntity>> GetAllAsync();

    Task<BidEntity> AddAsync(BidEntity entity);

    Task UpdateAsync(BidEntity entity);

    Task DeleteAsync(BidEntity entity);
}
