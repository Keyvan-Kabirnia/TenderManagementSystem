using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Domain.Entities;
public class Tender : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Deadline { get; set; }
    public decimal? EstimatedBudget { get; set; }

    // FKs
    public int CategoryId { get; set; }
    public int StatusId { get; set; }

    // Navigations
    public virtual Category Category { get; set; } = null!;
    public virtual Status Status { get; set; } = null!;
    public virtual List<Bid> Bids { get; set; } = [];

}
