using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tms.Domain.Entities;

namespace Tms.Persistence.EntityConfiguration;
internal class UserEntityConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> entity)
    {
        entity.ConfigureBaseEntity();

        entity.Property(e => e.UserName).IsRequired().HasMaxLength(50);
        entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
        entity.Property(e => e.PasswordHash).IsRequired().HasMaxLength(128);
        entity.Property(e => e.Role).IsRequired();

        entity.HasIndex(e => e.Id);
        entity.HasIndex(e => e.Email);
    }
}
