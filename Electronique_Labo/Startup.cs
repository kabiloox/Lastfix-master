using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Electronique_Labo.Startup))]
namespace Electronique_Labo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
