using Owin;

namespace Codetecuico.Byns.Api
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            Auth0Config.Configure(app);
        }
    }
}
