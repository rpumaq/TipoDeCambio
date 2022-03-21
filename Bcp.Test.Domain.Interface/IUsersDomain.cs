using Bcp.Test.Domain.Entity;

namespace Bcp.Test.Domain.Interface
{
    public interface IUsersDomain
    {
        Users Authenticate(string username, string password);
    }
}
