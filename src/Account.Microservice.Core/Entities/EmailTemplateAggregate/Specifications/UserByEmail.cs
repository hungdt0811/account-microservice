using Ardalis.Specification;
using Account.Microservice.Core.Entities.EmailTemplateAggregate;

namespace Account.Microservice.Core.Entities.UserAggregate.Specifications;

    public sealed class BySystemName : Specification<EmailTemplate>
    {
        public BySystemName(string systemName)
        {
            Query.Where(b => b.SystemName == systemName);
        }
    }

