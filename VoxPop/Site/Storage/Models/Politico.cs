namespace Site.Storage.Models
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using ElCamino.AspNet.Identity.AzureTable.Model;
    using Microsoft.AspNet.Identity;

    public class Politico : IdentityUser
    {
        /// <summary>
        /// Generated the <see cref="ClaimsIdentity"/> of this <see cref="Politico"/>.
        /// </summary>
        /// <param name="manager">The <see cref="UserManager{T}"/> or <see cref="Politico"/> entities.</param>
        /// <returns>The <see cref="ClaimsIdentity"/>.</returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<Politico> manager)
        {
            if (manager == null)
                throw new ArgumentNullException("manager");

            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            ClaimsIdentity userIdentity =
                await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            // Add custom user claims here
            return userIdentity;
        }

        public string AuthorFirstName { get; set; }

        public string AuthorLastName { get; set; }

        public static string GenerateNewUserName(string firstName, string lastName)
        {
            var username = firstName + lastName + Guid.NewGuid().ToString("N").Substring(0, 5);

            return username.ToLowerInvariant();
        }
    }
}