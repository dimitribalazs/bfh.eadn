using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BFH.EADN.UI.Web.Startup))]
namespace BFH.EADN.UI.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
