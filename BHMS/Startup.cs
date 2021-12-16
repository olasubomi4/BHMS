using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BHMS.Startup))]
namespace BHMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
