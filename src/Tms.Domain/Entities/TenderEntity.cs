using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Domain.Entities;
public class TenderEntity : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
    public decimal? EstimatedBudget { get; set; }

    // FKs
    public int OwnerId { get; set; }
    public int CategoryId { get; set; }
    public int StatusId { get; set; }

    // Navigations
    public virtual UserEntity Owner { get; set; } = null!;
    public virtual CategoryEntity Category { get; set; } = null!;
    public virtual StatusEntity Status { get; set; } = null!;
    public virtual List<BidEntity> Bids { get; set; } = [];
}
