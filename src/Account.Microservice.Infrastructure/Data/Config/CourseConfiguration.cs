using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Account.Microservice.Core.Entities.CoursesAggreate;
using Account.Microservice.Core.Constants;
using Account.Microservice.Core.Entities.SecurityAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Microservice.Infrastructure.Data.Config;
public class CourseConfiguration : IEntityTypeConfiguration<Course> 
{
  public void Configure(EntityTypeBuilder<Course> builder)
  {
    builder.Property(p => p.Id).UseIdentityColumn();

    builder.Property(p => p.Code).HasMaxLength(50);
    builder.Property(u => u.Code).IsRequired();
    builder.HasIndex(u => u.Code).IsUnique();

    builder.Property(p => p.Name).HasMaxLength(50);
    builder.Property(p => p.Name).IsRequired();

    builder.Property(p => p.Status).IsRequired().HasDefaultValue(CourseStatus.NotActive);

    builder.Property(p => p.Type).IsRequired().HasDefaultValue(CourseType.Free);

    builder.Property(p =>p.LecturerId).IsRequired();

    builder.Property(p => p.Rating).HasDefaultValue(0);

    builder.Property(p => p.IsCertificate).HasDefaultValue(0);

    builder.Property(p => p.TotalTimeVideo).HasDefaultValue(0);

    builder.Property(p => p.ImgPath).HasMaxLength(250);
    builder.Property(p => p.Language).HasMaxLength(50);
    builder.Property(p => p.IntroVideo).HasMaxLength(250);
    builder.Property(p => p.ImgBanner).HasMaxLength(250);
    builder.Property(p => p.Slug).HasMaxLength(250);

    builder.HasOne(c => c.Lecturer)
            .WithMany(u => u.Courses)
            .HasForeignKey(c => c.LecturerId)
            .OnDelete(DeleteBehavior.Cascade);
  }
}
