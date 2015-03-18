using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NewsEngine.Startup))]
namespace NewsEngine
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
