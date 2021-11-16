using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BHMS2._0.Startup))]
namespace BHMS2._0
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
