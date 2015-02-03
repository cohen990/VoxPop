//-----------------------------------------------------------------------
// <copyright company="MoPowered">
//     Copyright (c) MoPowered. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Site.Models
{
    using System.Configuration;
    using ElCamino.AspNet.Identity.AzureTable;
    using ElCamino.AspNet.Identity.AzureTable.Model;

    /// <summary>
    /// The authentication context.
    /// </summary>
    public class AuthenticationContext : IdentityCloudContext<Politico>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationContext"/> class.
        /// </summary>
        public AuthenticationContext()
            : base(GetIdentityConfiguration())
        {
        }

        /// <summary>
        /// Gets the <see cref="IdentityConfiguration"/>.
        /// </summary>
        /// <returns>The <see cref="IdentityConfiguration"/>.</returns>
        private static IdentityConfiguration GetIdentityConfiguration()
        {
            return new IdentityConfiguration
            {
                StorageConnectionString = ConfigurationManager.AppSettings["voxpop.authconnectionstring"],
            };
        }
    }
}
