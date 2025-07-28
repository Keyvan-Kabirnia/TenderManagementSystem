using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tms.Domain.Entities;

namespace Tms.Persistence.EntityConfiguration;
internal class VendorEntityConfiguration : IEntityTypeConfiguration<VendorEntity>
{
    public void Configure(EntityTypeBuilder<VendorEntity> entity)
    {
        entity.ConfigureBaseEntity();

        entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
        entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
        entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
        entity.Property(e => e.Address).IsRequired().HasMaxLength(500);
    }
}
