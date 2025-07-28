using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tms.Domain.Entities;

namespace Tms.Persistence.Data;
public partial class TmsDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<StatusEntity> Statuses { get; set; }
    public DbSet<VendorEntity> Vendors { get; set; }
    public DbSet<TenderEntity> Tenders { get; set; }
    public DbSet<BidEntity> Bids { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        SeedData(modelBuilder);
    }
}
