using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tms.Domain.Entities;

namespace Tms.Persistence.EntityConfiguration;
internal static class BaseEntityConfiguration
{
    public static void ConfigureBaseEntity<T>(this EntityTypeBuilder<T> entity) where T : BaseEntity
    {
        entity.HasKey(e => e.Id);
        entity.Property(e => e.CreatedAt).IsRequired();
    }
}