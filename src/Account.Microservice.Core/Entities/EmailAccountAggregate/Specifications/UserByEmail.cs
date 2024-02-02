using Ardalis.Specification;

namespace Account.Microservice.Core.Entities.EmailAccountAggregate.Specifications;

    public sealed class EmailAccountDefault : Specification<EmailAccount>
    {
        public EmailAccountDefault()
        {
            Query.Where(b => b.IsDefault == true);
        }
    }

