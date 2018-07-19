using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Shared.Enumerations;
using Shared.Resources;
using StudentManagement.Interfaces.Services;

namespace StudentManagement.Attributes
{
    public class ApiRoleAttribute : AuthorizationFilterAttribute
    {
        #region MyRegion

        /// <summary>
        /// List of roles which can be used for access into system.
        /// </summary>
        private readonly UserRoles[] _roles;

        #endregion

        #region Constructors

        /// <summary>
        /// Initiate attribute with specifc roles.
        /// </summary>
        /// <param name="roles"></param>
        public ApiRoleAttribute(UserRoles[] roles)
        {
            _roles = roles;
        }

        #endregion

        #region MyRegion

        /// <summary>
        ///     Override this function for checking whether user is allowed to access function.
        /// </summary>
        /// <param name="httpActionContext"></param>
        /// <returns></returns>
        public override void OnAuthorization(HttpActionContext httpActionContext)
        {
            IIdentityService identityService = null;
            var dependencyScope = httpActionContext.Request.GetDependencyScope();

            // Find identity service.
            identityService = (IIdentityService)dependencyScope.GetService(typeof(IIdentityService));

            // Find identity from request.
            var profile = identityService.FindRequestIdentity(httpActionContext.Request);
            if (profile == null || _roles == null)
            {
                httpActionContext.Response =
                    httpActionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, HttpMessages.AccessDenied);
                return;
            }

            if (!_roles.Any(x => profile.Roles.Contains((int)x)))
            {
                httpActionContext.Response =
                    httpActionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, HttpMessages.AccessDenied);
                return;
            }
        }

        /// <summary>
        ///     Whether method or controller allows anonymous requests or not.
        /// </summary>
        /// <param name="httpActionContext"></param>
        /// <returns></returns>
        private bool IsAllowAnonymousRequest(HttpActionContext httpActionContext)
        {
            return httpActionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                   ||
                   httpActionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>
                       ().Any();
        }

        #endregion
    }
}