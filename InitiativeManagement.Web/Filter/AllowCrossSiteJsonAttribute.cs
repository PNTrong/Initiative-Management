using System.Web.Mvc;

namespace InitiativeManagement.Web.Filter
{
    public class AllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var res = filterContext.RequestContext.HttpContext.Response;
            var req = filterContext.RequestContext.HttpContext.Request;
            var origin = req.Headers["Origin"];
            //if (SettingsHelper.AllowedDomains.Contains(origin))
            //{
            res.AppendHeader("Access-Control-Allow-Origin", req.Headers["Origin"]);
            res.AppendHeader("Access-Control-Allow-Credentials", "true");
            res.AppendHeader("Access-Control-Allow-Headers", "Content-Type, X-CSRF-Token, X-Requested-With, Accept, Accept-Version, Content-Length, Content-MD5, Date, X-Api-Version, X-File-Name");
            res.AppendHeader("Access-Control-Allow-Methods", "POST,GET,PUT,PATCH,DELETE,OPTIONS");
            if (req.HttpMethod == "OPTIONS")
            {
                res.StatusCode = 200;
                res.End();
            }
            //}

            base.OnActionExecuting(filterContext);
        }
    }
}