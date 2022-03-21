using Bcp.Test.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bcp.Test.Infrastructure.Interface
{
    public interface IUsersRepository
    {
        Users Authenticate(string username, string password);
    }
}
