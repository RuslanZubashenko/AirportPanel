using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Airport.MVC.Startup))]
namespace Airport.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
