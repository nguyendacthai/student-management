using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Database.Enumerations;
using Shared.Enumerations;
using Shared.Models;
using Shared.Resources;
using StudentManagement.Attributes;
using StudentManagement.Enumerations;
using StudentManagement.Interfaces.Repositories;
using StudentManagement.Interfaces.Services;
using StudentManagement.ViewModels.Class;

namespace StudentManagement.Controllers.Class
{
    [RoutePrefix("api/class")]
    public class ClassController : ApiBasicController
    {
        #region Constructor

        /// <summary>
        /// Initiate controller with injectors.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="identityService"></param>
        /// <param name="systemTimeService"></param>
        public ClassController(IUnitOfWork unitOfWork, IIdentityService identityService, ISystemTimeService systemTimeService) : base(unitOfWork, identityService, systemTimeService)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create new class
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ApiRole(new[]
        {
            UserRoles.Admin
        })]
        public async Task<IHttpActionResult> CreateClass([FromBody] AddClassViewModel info)
        {
            #region Parameter validation

            if (info == null)
            {
                info = new AddClassViewModel();
                Validate(info);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #endregion

            var classes = UnitOfWork.RepositoryClass.Search();
            classes =
                classes.Where(x => x.Name.Equals(info.Name, StringComparison.InvariantCultureIgnoreCase));

            // Class exists.
            if (await classes.AnyAsync())
                return ResponseMessage(
                    Request.CreateErrorResponse(HttpStatusCode.Conflict, HttpMessages.CannotBeDuplicated));

            var objClass = new Database.Models.Entities.Class
            {
                SpecializedId = info.SpecializedId,
                Name = info.Name,
                Status = MasterItemStatus.Active
            };

            objClass = UnitOfWork.RepositoryClass.Insert(objClass);

            await UnitOfWork.CommitAsync();

            return Ok(objClass);
        }

        /// <summary>
        /// Update class
        /// </summary>
        /// <param name="id"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        [ApiRole(new[]
        {
            UserRoles.Admin
        })]
        public async Task<IHttpActionResult> EditClass([FromUri] int id, [FromBody] EditClassViewModel info)
        {
            #region Parameter validation

            if (info == null)
            {
                info = new EditClassViewModel();
                Validate(info);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #endregion

            #region Find class

            var classes = UnitOfWork.RepositoryClass.Search();

            var iClass = await classes.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (iClass == null)
            {
                return ResponseMessage(
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.ClassNotFound));
            }

            #endregion

            #region Update class information

            // Check whether information has been updated or not.
            var bHasInformationChanged = false;

            // Specialized id is specified
            if (info.SpecializedId != iClass.SpecializedId)
            {
                iClass.SpecializedId = info.SpecializedId;
                bHasInformationChanged = true;
            }

            // Name is specified.
            if (info.Name != null && info.Name != iClass.Name)
            {
                iClass.Name = info.Name;
                bHasInformationChanged = true;
            }

            // Status is defined.
            if (info.Status != null && info.Status != iClass.Status)
            {
                iClass.Status = info.Status.Value;
                bHasInformationChanged = true;
            }

            // Information has been changed. Update the date time.
            if (bHasInformationChanged)
            {
                // Save information into database.
                await UnitOfWork.CommitAsync();
            }

            #endregion

            return Ok(iClass);
        }

        /// <summary>
        /// Search for a list of class
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [Route("load-class")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> LoadClass(SearchClassViewModel info)
        {
            #region Parameter validation

            if (info == null)
            {
                info = new SearchClassViewModel();
                Validate(info);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #endregion

            // Get all class from database
            var classes = UnitOfWork.RepositoryClass.Search();

            // Id have been defined
            if (info.Ids != null && info.Ids.Count > 0)
            {
                info.Ids = info.Ids.Where(x => x > 0).ToList();
                if (info.Ids != null && info.Ids.Count > 0)
                {
                    classes = classes.Where(x => info.Ids.Contains(x.Id));
                }
            }

            // Name have been defined
            if (info.Names != null && info.Names.Count > 0)
            {
                info.Names = info.Names.Where(x => !string.IsNullOrEmpty(x)).ToList();
                if (info.Names.Count > 0)
                    classes = classes.Where(x => info.Names.Any(y => x.Name.Contains(y)));
            }

            // Specialized id have been defined
            if (info.SpecializedIds != null && info.SpecializedIds.Count > 0)
            {
                info.SpecializedIds = info.SpecializedIds.Where(x => x > 0).ToList();
                if (info.SpecializedIds != null && info.SpecializedIds.Count > 0)
                {
                    classes = classes.Where(x => info.SpecializedIds.Contains(x.SpecializedId));
                }
            }

            // Statuses have been defined.
            if (info.Statuses != null && info.Statuses.Count > 0)
            {
                info.Statuses =
                    info.Statuses.Where(x => Enum.IsDefined(typeof(MasterItemStatus), x)).ToList();
                if (info.Statuses.Count > 0)
                    classes = classes.Where(x => info.Statuses.Contains(x.Status));
            }

            // Do sorting.
            var sorting = info.Sort;
            if (sorting != null)
                classes = UnitOfWork.Sort(classes, sorting.Direction, sorting.Property);
            else
                classes = UnitOfWork.Sort(classes, SortDirection.Ascending,
                    ClassPropertySort.Name);

            // Paginate.
            var result = new SearchResult<IList<Database.Models.Entities.Class>>
            {
                Total = classes.Count(),
                Records = await UnitOfWork.Paginate(classes, info.Pagination).ToListAsync()
            };

            return Ok(result);
        }

        #endregion
    }
}