using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DevelopXamarinTestBackend.Startup))]
namespace DevelopXamarinTestBackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
