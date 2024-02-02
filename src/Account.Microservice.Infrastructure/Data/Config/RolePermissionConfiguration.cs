using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Entities.SecurityAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Account.Microservice.Infrastructure.Data.Config;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
  public void Configure(EntityTypeBuilder<RolePermission> builder)
  {
    builder.ToTable("RolePermission");

    builder.Property(r => r.Id).UseIdentityColumn();

    builder.HasOne(rp => rp.Role)
        .WithMany()
        .HasForeignKey(rp => rp.RoleId)
        .OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(rp => rp.Permission)
        .WithMany()
        .HasForeignKey(rp => rp.PermissionId)
        .OnDelete(DeleteBehavior.Cascade);
  }
}
