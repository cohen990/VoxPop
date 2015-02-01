using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VoxPop.Startup))]
namespace VoxPop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
