using InitiativeManagement.Web.Mappings;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace InitiativeManagement.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //protected void Application_BeginRequest(object sender, EventArgs e)
        //{
        //    var res = Context.Response;
        //    var req = Context.Request;
        //    var origin = req.Headers["Origin"];
        //    if (Context.Request.HttpMethod == "OPTIONS")
        //    {
        //        res.AppendHeader("Access-Control-Allow-Origin", req.Headers["Origin"]);
        //        res.AppendHeader("Access-Control-Allow-Credentials", "true");
        //        res.AppendHeader("Access-Control-Allow-Headers", "Content-Type, X-CSRF-Token, X-Requested-With, Accept, Accept-Version, Content-Length, Content-MD5, Date, X-Api-Version, X-File-Name");
        //        res.AppendHeader("Access-Control-Allow-Methods", "POST,GET,PUT,PATCH,DELETE,OPTIONS");
        //        res.StatusCode = 200;
        //        res.End();
        //    }
        //}

        protected void Application_Start()
        {
            LicenseHelper.ModifyInMemory.ActivateMemoryPatching();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //AutoMapperConfiguration.Configure();
            AutoMapper.Mapper.Initialize(c => c.AddProfile<AutoMapperConfiguration>());
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}