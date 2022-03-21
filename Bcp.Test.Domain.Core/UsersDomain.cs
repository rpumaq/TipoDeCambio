using Bcp.Test.Domain.Entity;
using Bcp.Test.Domain.Interface;
using Bcp.Test.Infrastructure.Interface;

namespace Bcp.Test.Domain.Core
{
    public class UsersDomain : IUsersDomain
    {
        private readonly IUsersRepository _usersRepository;
        public UsersDomain(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public Users Authenticate(string userName, string password)
        {
            return _usersRepository.Authenticate(userName, password);
        }
    }
}
