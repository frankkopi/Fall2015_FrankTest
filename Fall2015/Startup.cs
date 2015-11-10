using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fall2015.Startup))]
namespace Fall2015
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
