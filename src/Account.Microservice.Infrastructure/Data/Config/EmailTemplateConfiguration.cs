
using Account.Microservice.Core.Entities.EmailTemplateAggregate;
using Account.Microservice.Core.ProjectAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Account.Microservice.Infrastructure.Data.Config;

public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
{
  public void Configure(EntityTypeBuilder<EmailTemplate> builder)
  {
    builder.Property(p => p.Id).UseIdentityColumn();
    builder.Property(p => p.SystemName)
        .HasMaxLength(200)
        .IsRequired();

    builder.Property(p => p.EmailSubject)
        .HasMaxLength(300)
        .IsRequired();

    builder.Property(p => p.Content)
        .IsRequired();
  }
}
