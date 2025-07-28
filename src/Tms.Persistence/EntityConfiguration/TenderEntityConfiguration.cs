using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tms.Domain.Entities;

namespace Tms.Persistence.EntityConfiguration;
internal class TenderEntityConfiguration : IEntityTypeConfiguration<TenderEntity>
{
    public void Configure(EntityTypeBuilder<TenderEntity> entity)
    {
        entity.ConfigureBaseEntity();

        entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
        entity.Property(e => e.Description).IsRequired().HasMaxLength(2000);
        entity.Property(e => e.Deadline).IsRequired();

        entity.HasOne(e => e.Owner)
            .WithMany(e => e.Tenders)
            .HasForeignKey(e => e.OwnerId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.Category)
            .WithMany(e => e.Tenders)
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.Status)
            .WithMany(e => e.Tenders)
            .HasForeignKey(e => e.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
