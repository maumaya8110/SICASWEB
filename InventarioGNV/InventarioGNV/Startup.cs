using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(InventarioGNV.Startup))]
namespace InventarioGNV
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
