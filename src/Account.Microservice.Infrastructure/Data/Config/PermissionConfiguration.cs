using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Entities.SecurityAggregate;
using Account.Microservice.Core.ProjectAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Microservice.Infrastructure.Data.Config;
public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
  public void Configure(EntityTypeBuilder<Permission> builder)
  {
    builder.Property(p => p.Id).UseIdentityColumn();
    builder.Property(r => r.Name).IsRequired();
    builder.Property(r => r.Code).IsRequired();
    builder.HasMany(p => p.Children).WithOne().HasForeignKey(p => p.ParentId).IsRequired(false);
  }
}
