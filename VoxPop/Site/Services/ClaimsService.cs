namespace Site.Services
{
    using System.Linq;
    using System.Security.Claims;
    using System.Threading;
    using Storage.Models;

    public static class ClaimsService
    {
        public static string GetClaim(string claimKey)
        {
            var principal = (ClaimsPrincipal)Thread.CurrentPrincipal;

            var claim = principal.Claims
                .Where(x => x.Type == claimKey)
                .Select(x => x.Value)
                .SingleOrDefault();

            return claim;
        }

        public static string GetAuthenticatedUsersFullName()
        {
            var firstName = GetClaim(VoxPopConstants.FirstNameClaimKey);

            var lastName = GetClaim(VoxPopConstants.LastNameClaimKey);

            return Politico.GetFullName(firstName, lastName);
        }
    }
}