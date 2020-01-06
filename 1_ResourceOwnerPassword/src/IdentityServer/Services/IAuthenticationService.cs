using IdentityServer.Models;
using System;

namespace IdentityServer.Services
{
    public interface IAuthenticationService
    {
        TestUser ValidateUser(string username, string password);
        TestUser GetUser(Guid guid);
    }
}