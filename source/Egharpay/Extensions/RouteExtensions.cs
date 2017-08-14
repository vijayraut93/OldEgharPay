using System.Web.Mvc;
using System.Web.Routing;
using Egharpay.Constraints;
using Egharpay.Interfaces;

namespace Egharpay.Extensions
{
    public static class RouteExtensions
    {
        public static void MapRouteWithTenantConstraint(this RouteCollection routes, string name, string url, object defaults)
        {
            routes.MapRoute(
                name,
                url,
                defaults,
                new { TenantAccess = new TenantRouteConstraint(DependencyResolver.Current.GetService<ITenantsService>()) }
            );
        }
    }
}