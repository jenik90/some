using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace QULIX.WebUi
{
    using QULIX.Domain.QULEX.DataLayer.DbEntities;
    using QULIX.WebUi.App_Start;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            SourceFileConfig.RegisterSources(Server.MapPath("/bin"));
            LoggerConfig.RegisterLogger(Server.MapPath("~/Logging/info.txt"));
        }

        protected void Application_End()
        {
            LoggerConfig.DisposeLogger();
        }
    }
}
