using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LostTimeWeb.Startup))]
namespace LostTimeWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
