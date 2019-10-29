using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ptc.Seteng.Startup))]
namespace Ptc.Seteng
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
