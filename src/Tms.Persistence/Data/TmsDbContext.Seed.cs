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
            new() {Id=1, Name = "Category 1", Description = "Description of Category 1"},
            new() {Id=2, Name = "Category 2", Description = "Description of Category 2"},
            new() {Id=3, Name = "Category 3", Description = "Description of Category 3"},
            new() {Id=4, Name = "Category 4", Description = "Description of Category 4"},
        ];

    public static StatusEntity[] _statusEntitySeeds =
        [
            new() {Id=1, Name = "Open", Description = "Tender is open for biding.", Type = "Tender"},
            new() {Id=2, Name = "Close", Description = "Tender is close for biding.", Type = "Tender"},
            new() {Id=3, Name = "Canceled", Description = "Tender is canceled.", Type = "Tender"},

            new() {Id=4, Name = "Pending", Description = "Bid is pending for review.", Type = "Bid"},
            new() {Id=5, Name = "Approved", Description = "Bid is approved.", Type = "Bid"},
            new() {Id=6, Name = "Rejected", Description = "Bid is rejected.", Type = "Bid"},
            new() {Id=7, Name = "Withdrawn", Description = "Bid is withdrawn.", Type = "Bid"},
        ];

    public static UserEntity[] _admins =
        [
            new()
            {
                Id = 1,
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
