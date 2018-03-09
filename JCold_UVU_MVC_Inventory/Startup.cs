using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JCold_UVU_MVC_Inventory.Startup))]
namespace JCold_UVU_MVC_Inventory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
