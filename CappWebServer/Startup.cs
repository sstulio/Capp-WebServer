using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CappWebServer.Startup))]
namespace CappWebServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
