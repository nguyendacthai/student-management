using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using AutoMapper;
using Database.Models.Entities;
using StudentManagement.Interfaces.Repositories;
using StudentManagement.Interfaces.Services;
using StudentManagement.ViewModels.Account;

namespace StudentManagement.Attributes
{
    public class ApiAuthorizeAttribute : AuthorizationFilterAttribute
    {
        #region Methods
        
        /// <summary>
        ///     Override this function for checking whether user is allowed to access function.
        /// </summary>
        /// <param name="httpActionContext"></param>
        /// <returns></returns>
        public override void OnAuthorization(HttpActionContext httpActionContext)
        {
            ProfileViewModel profile = null;
            IIdentityService identityService = null;
            var dependencyScope = httpActionContext.Request.GetDependencyScope();

            #region Principle validation

            // Find identity service.
            identityService = (IIdentityService)dependencyScope.GetService(typeof(IIdentityService));
            //var memoryCacheService = (IMemoryCacheService)dependencyScope.GetService(typeof(IMemoryCacheService));

            // Get profile cache service.
            var profileCacheService =
                (IValueCacheService<int, ProfileViewModel>)dependencyScope.GetService(
                    typeof(IValueCacheService<int, ProfileViewModel>));

            // FullSearch the principle of request.
            var principle = httpActionContext.RequestContext.Principal;

            // Principal is invalid.
            if (principle == null)
            {
                // Authentication allow. No need to check identity.
                if (IsAllowAnonymousRequest(httpActionContext))
                    return;

                httpActionContext.Response =
                    httpActionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
                        "INVALID_AUTHENTICATION_TOKEN");
                return;
            }

            // FullSearch the identity set in principle.
            var identity = principle.Identity;
            if (identity == null)
            {
                // Authentication allow. No need to check identity.
                if (IsAllowAnonymousRequest(httpActionContext))
                    return;

                httpActionContext.Response =
                    httpActionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
                        "INVALID_AUTHENTICATION_TOKEN");
                return;
            }

            #endregion

            #region Claim identity

            // FullSearch the claim identity.
            var claimIdentity = (ClaimsIdentity)identity;

            // Claim doesn't contain studentId.
            var userId = claimIdentity.FindFirst(nameof(Student.Id));
            if (string.IsNullOrEmpty(userId?.Value))
            {
                // Authentication allow. No need to check identity.
                if (IsAllowAnonymousRequest(httpActionContext))
                    return;

                httpActionContext.Response =
                    httpActionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
                        "INVALID_AUTHENTICATION_TOKEN");
                return;
            }

            // Find profile from cache by using user id.
            var iUserId = Convert.ToInt32(userId.Value);
            profile = profileCacheService.Read(iUserId);

            // Profile has been found in Request.
            if (profile != null)
            {
                identityService.InitRequestIdentity(httpActionContext.Request, profile);
                return;
            }

            // Find unit of work from lifetime scope.
            var unitOfWork = (IUnitOfWork)dependencyScope.GetService(typeof(IUnitOfWork));

            var users = unitOfWork.RepositoryStudent.Search();
            users = users.Where(x =>
                x.Id.ToString().Equals(userId.Value, StringComparison.InvariantCultureIgnoreCase));

            // Find the first matched user.
            var user = users.FirstOrDefault();
            if (user == null)
            {
                // Authentication allow. No need to check identity.
                if (IsAllowAnonymousRequest(httpActionContext))
                    return;

                httpActionContext.Response =
                    httpActionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
                        "INVALID_AUTHENTICATION_TOKEN");
                return;
            }

            // Find account roles.
            var roles = unitOfWork.RepositoryUserRole.Search();
            roles = roles.Where(x => x.StudentId == user.Id);

            // Find list of roles.
            // Set identity to request for future use.

            profile = Mapper.Map<Student, ProfileViewModel>(user);
            profile.Roles = roles.Select(x => x.RoleId).ToList();
            identityService.InitRequestIdentity(httpActionContext.Request, profile);

            #endregion

            // Insert account information into HttpItem for later use.
            // Push profile into cache.
            profileCacheService.Add(user.Id, profile);

            // Insert account information into HttpItem for later use.
//            var properties = httpActionContext.Request.Properties;
//            if (properties.ContainsKey(ClaimTypes.Actor))
//                properties[ClaimTypes.Actor] = profile;
//            properties.Add(ClaimTypes.Actor, profile);

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