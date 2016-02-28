using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyWebAppMVC.WebUI.Startup))]
namespace MyWebAppMVC.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
