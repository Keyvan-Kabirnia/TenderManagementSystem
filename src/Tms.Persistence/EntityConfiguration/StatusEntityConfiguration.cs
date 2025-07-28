using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tms.Domain.Entities;

namespace Tms.Persistence.EntityConfiguration;
internal class StatusEntityConfiguration : IEntityTypeConfiguration<StatusEntity>
{
    public void Configure(EntityTypeBuilder<StatusEntity> entity)
    {
        entity.ConfigureBaseEntity();

        entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
        entity.Property(e => e.Description).HasMaxLength(200);
        entity.Property(e => e.Type).IsRequired().HasMaxLength(20);
    }
}
