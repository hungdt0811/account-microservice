using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Entities.EmailAccountAggregate;
using Account.Microservice.Core.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Microservice.Infrastructure.Data.Config;
public class EmailRelatedConfiguration : IEntityTypeConfiguration<EmailRelated>
{
  public void Configure(EntityTypeBuilder<EmailRelated> builder)
  {
    builder.Property(r=>r.Id).UseIdentityColumn();
    builder.Property(r => r.Email).IsRequired();
    builder.HasOne(r => r.User).WithMany().HasForeignKey(r => r.UserId);
  }
}
