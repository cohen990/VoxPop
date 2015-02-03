using Microsoft.Owin;
using Site;

[assembly: OwinStartup(typeof(Startup))]
namespace Site
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
