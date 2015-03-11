namespace Site.Services
{
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity.Owin;
    using Storage.Models;

    public static class SignInManagerExtensions
    {
        public static async Task<SignInStatus> SignInWithEmailAsync(
            this SignInManager<Politico, string> manager,
            string emailAddress,
            string password,
            bool isPersistent,
            bool shouldLockout)
        {
            if (manager.UserManager == null)
            {
                return SignInStatus.Failure;
            }

            var user = await manager.UserManager.FindByNameAsync("aaronjerez2ae3b");

            if (user == null)
            {
                return SignInStatus.Failure;
            }

            return await manager.PasswordSignInAsync(user.UserName, password, isPersistent, shouldLockout);
        }
    }
}