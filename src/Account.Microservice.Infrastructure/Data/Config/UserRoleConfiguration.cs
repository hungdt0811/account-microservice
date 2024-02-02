using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Account.Microservice.Core.Entities.SecurityAggregate;

namespace Account.Microservice.Infrastructure.Data.Config;

public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
  public void Configure(EntityTypeBuilder<UserRole> builder)
  {
    builder.ToTable("UserRole");

    builder.Property(r => r.Id).UseIdentityColumn();

    builder.HasOne(rp => rp.User).WithMany().HasForeignKey(rp => rp.UserId).OnDelete(DeleteBehavior.Cascade);

    builder.HasOne(rp => rp.Role).WithMany().HasForeignKey(rp => rp.RoleId).OnDelete(DeleteBehavior.Cascade);
  }
}
