using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Domain.Entities;
public class StatusEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty; // "Tender"/"Bid"

    // Navigations
    public virtual List<TenderEntity> Tenders { get; set; } = [];
    public virtual List<BidEntity> Bids { get; set; } = [];
}
