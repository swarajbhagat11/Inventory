using Inventory.Models;
using Inventory.Repositories.Interfaces;
using Inventory.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Inventory.Filters
{
    public class IsAdminFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly IAccessService _accessService;

        public IsAdminFilter(IAccessService accessService)
        {
            _accessService = accessService;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.Request.Headers.ContainsKey("access-id") || !context.HttpContext.Request.Headers.ContainsKey("secret-key"))
            {
                context.Result = new BadRequestObjectResult("Please pass access-id and secret-key values in header.");
            }
            else if (!_accessService.IsAdmin(context.HttpContext.Request.Headers["access-id"], context.HttpContext.Request.Headers["secret-key"]))
            {
                context.Result = new BadRequestObjectResult("You are not authorize to access this API.");
            }
        }
    }
}
