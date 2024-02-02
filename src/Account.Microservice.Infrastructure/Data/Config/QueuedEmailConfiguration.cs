using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Entities.SecurityAggregate;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Account.Microservice.Core.Entities.QueuedEmailAggregate;

namespace Account.Microservice.Infrastructure.Data.Config;
internal class QueuedEmailConfiguration : IEntityTypeConfiguration<QueuedEmail>
{
  public void Configure(EntityTypeBuilder<QueuedEmail> builder)
  {
    builder.Property(r => r.Id).UseIdentityColumn();
    builder.Property(r => r.To).IsRequired();
    builder.Property(r => r.From).IsRequired();
    builder.Property(r => r.FromName).IsRequired();
    builder.Property(r => r.Subject).IsRequired();
    builder.Property(r => r.Body).IsRequired();
  }
}

