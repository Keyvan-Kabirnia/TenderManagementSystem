using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Domain.Entities;
public class BidEntity : BaseEntity
{
    public decimal Amount { get; set; }
    public DateTime SubmissionDate { get; set; }

    // FKs
    public int StatusId { get; set; }
    public int TenderId { get; set; }
    public int VendorId { get; set; }

    // Navigations
    public virtual StatusEntity Status { get; set; } = null!;
    public virtual TenderEntity Tender { get; set; } = null!;
    public virtual VendorEntity Vendor { get; set; } = null!;
}
