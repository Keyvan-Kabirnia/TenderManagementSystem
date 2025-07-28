using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tms.Domain.Entities;

namespace Tms.Persistence.EntityConfiguration;
internal class BidEntityConfiguration : IEntityTypeConfiguration<BidEntity>
{
    public void Configure(EntityTypeBuilder<BidEntity> entity)
    {
        entity.ConfigureBaseEntity();

        entity.Property(e => e.Amount).IsRequired().HasColumnType("decimal(18,2)");
        entity.Property(e => e.SubmissionDate).IsRequired();

        entity.HasOne(e => e.Tender)
            .WithMany(e => e.Bids)
            .HasForeignKey(e => e.TenderId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.Vendor)
            .WithMany(e => e.Bids)
            .HasForeignKey(e => e.VendorId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(e => e.Status)
            .WithMany(e => e.Bids)
            .HasForeignKey(e => e.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
