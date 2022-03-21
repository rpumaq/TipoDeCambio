using Bcp.Test.Application.DTO;
using Bcp.Test.Transversal.Common;

namespace Bcp.Test.Application.Interface
{
    public interface IUsersApplication
    {
        Response<UsersDto> Authenticate(string username, string password);
    }
}
