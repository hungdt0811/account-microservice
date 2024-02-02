using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Entities.EmailAccountAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Microservice.Infrastructure.Data.Config;
public class EmailAccountConfiguration : IEntityTypeConfiguration<EmailAccount>
{
  public void Configure(EntityTypeBuilder<EmailAccount> builder)
  {
    builder.Property(r=>r.Id).UseIdentityColumn();
    builder.Property(r => r.UserName).IsRequired();
    builder.Property(r => r.Password).IsRequired();
    builder.Property(r => r.Host).IsRequired();
    builder.Property(r => r.Port).IsRequired();
    builder.Property(r => r.Ssl).IsRequired();
  }
}
