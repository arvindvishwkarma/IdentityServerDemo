using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer.Models;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace IdentityServer.Services
{
    /// <summary>
    /// This interface allows IdentityServer to connect to your user and profile store.
    /// </summary>
    public class UserProfileService : IProfileService
    {
        private readonly IAuthenticationService _authenticationService;

        public UserProfileService(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subject = context.Subject ?? throw new ArgumentNullException(nameof(context.Subject));
            var subjectId = Guid.Parse(subject.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject)?.Value ?? throw new InvalidOperationException());

            var user = _authenticationService.GetUser(subjectId);
            var claims = GetClaimsFromUser(user);
            context.IssuedClaims = claims.ToList();
            return Task.FromResult(context);
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var userGuid = Guid.Parse(context.Subject.GetSubjectId());
            var user = _authenticationService.GetUser(userGuid);
            context.IsActive = user != null && user.Active;
            return Task.CompletedTask;
        }

        private IEnumerable<Claim> GetClaimsFromUser(TestUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Subject, user.Guid.ToString()),
            };

            if (!string.IsNullOrWhiteSpace(user.FirstName))
                claims.Add(new Claim("name", user.FirstName));

            if (!string.IsNullOrWhiteSpace(user.LastName))
                claims.Add(new Claim("last_name", user.LastName));

            return claims;
        }
    }
}