using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Entities.SecurityAggregate;
using Account.Microservice.Core.ProjectAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Microservice.Infrastructure.Data.Config;
public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
  public void Configure(EntityTypeBuilder<Role> builder)
  {
    builder.Property(r => r.Id).UseIdentityColumn();

    builder.HasMany(r => r.Permissions)
            .WithMany()
            .UsingEntity<RolePermission>(
                j => j.HasOne(rp => rp.Permission).WithMany().HasForeignKey(rp => rp.PermissionId),
                j => j.HasOne(rp => rp.Role).WithMany().HasForeignKey(rp => rp.RoleId),
                j =>
                {
                  j.ToTable("RolePermission");
                });
  }
}
