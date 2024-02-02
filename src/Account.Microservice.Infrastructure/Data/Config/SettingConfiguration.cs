using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Entities.EmailAccountAggregate;
using Account.Microservice.Core.Entities.SettingAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Microservice.Infrastructure.Data.Config;
public class SettingConfiguration: IEntityTypeConfiguration<Setting>
{
  public void Configure(EntityTypeBuilder<Setting> builder)
  {
    builder.Property(r => r.Id).UseIdentityColumn();
    builder.Property(r => r.Name).IsRequired();
    builder.Property(r => r.Value).IsRequired();
  }
}
