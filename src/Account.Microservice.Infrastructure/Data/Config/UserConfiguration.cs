using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Constants;
using Account.Microservice.Core.Entities.SecurityAggregate;
using Account.Microservice.Core.Entities.UserAggregate;
using Account.Microservice.Core.ProjectAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Microservice.Infrastructure.Data.Config;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.Property(u => u.Id).UseIdentityColumn();
    builder.Property(u => u.FullName).HasMaxLength(50);
    builder.Property(u => u.Email).IsRequired();
    builder.HasIndex(u => u.Email).IsUnique();
    builder.Property(u => u.Status).IsRequired().HasDefaultValue(UserStatus.Deactive);
    builder.Property(u => u.Type).IsRequired().HasDefaultValue(UserType.Student);

    builder.HasMany(r => r.Roles)
        .WithMany()
        .UsingEntity<UserRole>(
            j => j.HasOne(rp => rp.Role).WithMany().HasForeignKey(rp => rp.RoleId),
            j => j.HasOne(rp => rp.User).WithMany().HasForeignKey(rp => rp.UserId),
            j =>
            {
              j.ToTable("UserRole");
            });
    builder.HasMany(u => u.Courses)
      .WithOne(c => c.Lecturer)
      .HasForeignKey(c => c.LecturerId);
  }
}
