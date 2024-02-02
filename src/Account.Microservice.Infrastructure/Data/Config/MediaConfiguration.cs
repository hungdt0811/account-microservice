using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Entities.EmailTemplateAggregate;
using Account.Microservice.Core.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Microservice.Infrastructure.Data.Config;
public class MediaConfiguration : IEntityTypeConfiguration<Media>
{
  public void Configure(EntityTypeBuilder<Media> builder)
  {
    builder.Property(p => p.Id).UseIdentityColumn();
  }
}
