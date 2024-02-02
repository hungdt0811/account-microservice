using Ardalis.Specification;
namespace Account.Microservice.Core.Entities.UserAggregate.Specifications;
public sealed class UserPhoneSpecification : Specification<User>
    {
        /// <summary>
        /// Query user by phone number
        /// </summary>
        /// <param name="phone"></param>

        public UserPhoneSpecification(string phone)
        {
            Query
                .Where(b => b.Mobile == phone);
        }
    }
