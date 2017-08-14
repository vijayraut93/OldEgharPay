﻿using System.Web;
using System.Web.Routing;
using Egharpay.Interfaces;

namespace Egharpay.Constraints
{
    public class TenantRouteConstraint : IRouteConstraint
    {
        private ITenantsService _tenantsService;
        public TenantRouteConstraint(ITenantsService tenantsService)
        {
            _tenantsService = tenantsService;
        }

        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return _tenantsService.CurrentTenantOrganisation(httpContext.Request.Url.Host) != null;
        }
    }
}