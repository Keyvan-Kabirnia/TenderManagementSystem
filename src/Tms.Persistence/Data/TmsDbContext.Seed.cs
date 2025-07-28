using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tms.Domain.Entities;

namespace Tms.Persistence.Data;
public partial class TmsDbContext
{
    public static CategoryEntity[] _categoryEntitySeeds =
        [
            new() { Name = "Category 1", Description = "Description of Category 1"},
            new() { Name = "Category 2", Description = "Description of Category 2"},
            new() { Name = "Category 3", Description = "Description of Category 3"},
            new() { Name = "Category 4", Description = "Description of Category 4"},
        ];

    public static StatusEntity[] _statusEntitySeeds =
        [
            new() { Name = "Open", Description = "Tender is open for biding.", Type = "Tender"},
            new() { Name = "Close", Description = "Tender is close for biding.", Type = "Tender"},
            new() { Name = "Canceled", Description = "Tender is canceled.", Type = "Tender"},

            new() { Name = "Pending", Description = "Bid is pending for review.", Type = "Bid"},
            new() { Name = "Approved", Description = "Bid is approved.", Type = "Bid"},
            new() { Name = "Rejected", Description = "Bid is rejected.", Type = "Bid"},
            new() { Name = "Withdrawn", Description = "Bid is withdrawn.", Type = "Bid"},
        ];

    public static UserEntity[] _admins =
        [
            new()
            {
                UserName = "Admin",
                Email = "Admin@TenderManagement.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin123!"),
                Role= Domain.Enums.UserRole.Admin,
                CreatedAt=DateTime.UtcNow
            }
        ];

    private void SeedData(ModelBuilder builder)
    {
        builder.Entity<CategoryEntity>()
            .HasData(_categoryEntitySeeds);

        builder.Entity<StatusEntity>()
            .HasData(_statusEntitySeeds);

        builder.Entity<UserEntity>()
            .HasData(_admins);
    }
}
