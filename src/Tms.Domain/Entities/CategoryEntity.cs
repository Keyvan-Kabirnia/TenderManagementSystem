using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tms.Domain.Entities;
public class CategoryEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    // Navigations
    public virtual List<TenderEntity> Tenders { get; set; } = [];
}
