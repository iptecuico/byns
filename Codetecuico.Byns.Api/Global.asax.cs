using Codetecuico.Byns.Api.Mappings;
using System.Web.Http;

namespace Codetecuico.Byns.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            AutofacConfig.Configure();
            AutoMapperConfiguration.Configure();
        }
    }
}
