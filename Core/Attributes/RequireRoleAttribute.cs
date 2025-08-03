using Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Core.Attributes
{
    public class RequireRoleAttribute : Attribute, IAuthorizationFilter
    {
        private readonly string[] _roles;

        public RequireRoleAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;

            if (!user.Identity?.IsAuthenticated ?? true)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userRole = user.FindFirst("Role")?.Value;
            
            if (string.IsNullOrEmpty(userRole) || !_roles.Contains(userRole))
            {
                context.Result = new ForbidResult();
            }
        }
    }

    public class AdminOnlyAttribute : RequireRoleAttribute
    {
        public AdminOnlyAttribute() : base(RoleConstants.Admin)
        {
        }
    }

    public class ClientOrAdminAttribute : RequireRoleAttribute
    {
        public ClientOrAdminAttribute() : base(RoleConstants.Client, RoleConstants.Admin)
        {
        }
    }
}