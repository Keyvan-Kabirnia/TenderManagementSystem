using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Domain.Entities;
public class Bid : BaseEntity
{
    public decimal Amount { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime SubmissionDate { get; set; }

    // FKs
    public int StatusId { get; set; }
    public int TenderId { get; set; }
    public int VendorId { get; set; }

    // Navigations
    public virtual Status Status { get; set; } = null!;
    public virtual Tender Tender { get; set; } = null!;
    public virtual Vendor Vendor { get; set; } = null!;
}
