namespace HNBlog.Web.Mvc.Controllers
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteRegistrar
    {
        public static void RegisterRoutesTo(RouteCollection routes) 
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            routes.MapRoute(
                "Blog",                                              // Route name
                "Blog/{action}/{id}",                           // URL with parameters
                new { controller = "Blog", action = "Blog", id = UrlParameter.Optional }); // Parameter defaults
            routes.MapRoute(
                "Details",                                              // Route name
                "Details/{action}/{id}",                           // URL with parameters
                new { controller = "Blog", action = "Details", id = UrlParameter.Optional }); // Parameter defaults

            routes.MapRoute(
                "Post",                                              // Route name
                "Blog/{action}/{id}",                           // URL with parameters
                new { controller = "Blog", action = "Post", id = UrlParameter.Optional }); // Parameter defaults

            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}/{id}",                           // URL with parameters
                new { controller = "Blog", action = "Post", id = UrlParameter.Optional }); // Parameter defaults
        }
    }
}
