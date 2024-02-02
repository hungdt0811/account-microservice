using Ardalis.Specification;

namespace Account.Microservice.Core.Entities.UserAggregate.Specifications;

    public sealed class UserByEmail : Specification<User>
    {
        public UserByEmail(string email)
        {
            Query.Where(b => b.Email == email);
        }
    }

