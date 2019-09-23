using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarketShow.Startup))]
namespace MarketShow
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
