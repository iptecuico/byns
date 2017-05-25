using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Codetecuico.Byns.Api.Startup))]
namespace Codetecuico.Byns.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}