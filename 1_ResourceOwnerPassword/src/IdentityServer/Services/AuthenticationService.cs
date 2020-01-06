using IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IdentityServer.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public TestUser ValidateUser(string username, string password)
        {
            return GetUsers().FirstOrDefault(m =>
                m.Username.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                m.Password.Equals(password, StringComparison.OrdinalIgnoreCase));
        }

        public TestUser GetUser(Guid guid)
        {
            return GetUsers().FirstOrDefault(m => m.Guid == guid);
        }

        private IEnumerable<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    Id = 1,
                    Guid = new Guid("b8ab296e-6f48-4dab-ba45-ad121bc884c3"),
                    FirstName = "James",
                    LastName = "Doe",
                    Username = "james",
                    Password = "password",
                    Active = true
                },
                new TestUser
                {
                    Id = 2,
                    Guid = new Guid("843d6e61-10a4-4b49-913d-c040c2ff419f"),
                    FirstName = "Cody",
                    LastName = "Lawton",
                    Username = "cody",
                    Password = "password",
                    Active = false
                }
            };
        }
    }
}