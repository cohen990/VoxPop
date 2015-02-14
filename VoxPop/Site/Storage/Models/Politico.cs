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
        /// Generated the <see cref="ClaimsIdentity"/> of this <see cref="Retailer"/>.
        /// </summary>
        /// <param name="manager">The <see cref="UserManager{T}"/> or <see cref="Retailer"/> entities.</param>
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
    }
}