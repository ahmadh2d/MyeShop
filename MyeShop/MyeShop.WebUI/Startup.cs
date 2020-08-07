using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyeShop.WebUI.Startup))]
namespace MyeShop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
