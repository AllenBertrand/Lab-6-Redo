using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CommunityInfoSite.Startup))]
namespace CommunityInfoSite
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
