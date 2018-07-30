using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Database.Enumerations;
using Shared.Enumerations;
using Shared.Models;
using StudentManagement.Enumerations;
using StudentManagement.Interfaces.Repositories;
using StudentManagement.Interfaces.Services;
using StudentManagement.ViewModels.Class;
using StudentManagement.ViewModels.UserRole;

namespace StudentManagement.Controllers.UserRole
{
    [RoutePrefix("api/user-role")]
    public class UserRoleController :  ApiBasicController
    {
        #region Construtor

        /// <summary>
        /// Initiate controller with injectors.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="identityService"></param>
        /// <param name="systemTimeService"></param>
        public UserRoleController(IUnitOfWork unitOfWork, IIdentityService identityService, ISystemTimeService systemTimeService) : base(unitOfWork, identityService, systemTimeService)
        {
        }

        #endregion

        #region Methods


        /// <summary>
        /// Search for a list of user role
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [Route("load-user-role")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> LoadUserRole(SearchUserRoleViewModel info)
        {
            #region Parameter validation

            if (info == null)
            {
                info = new SearchUserRoleViewModel();
                Validate(info);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #endregion

            // Get all user role from database
            var userRoles = UnitOfWork.RepositoryUserRole.Search();

            // Id have been defined
            if (info.Ids != null && info.Ids.Count > 0)
            {
                info.Ids = info.Ids.Where(x => x > 0).ToList();
                if (info.Ids != null && info.Ids.Count > 0)
                {
                    userRoles = userRoles.Where(x => info.Ids.Contains(x.Id));
                }
            }

            // Student id have been defined
            if (info.StudentIds != null && info.StudentIds.Count > 0)
            {
                info.StudentIds = info.StudentIds.Where(x => x > 0).ToList();
                if (info.StudentIds != null && info.StudentIds.Count > 0)
                {
                    userRoles = userRoles.Where(x => info.StudentIds.Contains(x.StudentId));
                }
            }

            // Do sorting.
            var sorting = info.Sort;
            if (sorting != null)
                userRoles = UnitOfWork.Sort(userRoles, sorting.Direction, sorting.Property);
            else
                userRoles = UnitOfWork.Sort(userRoles, SortDirection.Ascending,
                    UserRolePropertySort.Id);

            // Paginate.
            var result = new SearchResult<IList<Database.Models.Entities.UserRole>>
            {
                Total = userRoles.Count(),
                Records = await UnitOfWork.Paginate(userRoles, info.Pagination).ToListAsync()
            };

            return Ok(result);
        }

        #endregion

    }
}