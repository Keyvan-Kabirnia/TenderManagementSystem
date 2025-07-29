using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Domain.Entities;
public class VendorEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    // FKs
    public int UserId { get; set; }

    // Navigations
    public virtual UserEntity User { get; set; }
    public virtual List<BidEntity> Bids { get; set; } = [];

}
