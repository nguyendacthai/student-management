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
using StudentManagement.ViewModels.Specialized;

namespace StudentManagement.Controllers.Specialized
{
    [RoutePrefix("api/specialized")]
    public class SpecializedController : ApiBasicController
    {
        #region Constructor

        /// <summary>
        /// Initiate controller with injectors.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="identityService"></param>
        /// <param name="systemTimeService"></param>
        public SpecializedController(IUnitOfWork unitOfWork, IIdentityService identityService, ISystemTimeService systemTimeService) : base(unitOfWork, identityService, systemTimeService)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Create new specialized
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [ApiRole(new[]
        {
            UserRoles.Admin
        })]
        public async Task<IHttpActionResult> CreateSpecialized([FromBody] AddSpecializedViewModel info)
        {
            #region Parameter validation

            if (info == null)
            {
                info = new AddSpecializedViewModel();
                Validate(info);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #endregion

            var specializeds = UnitOfWork.RepositorySpecialized.Search();
            specializeds =
                specializeds.Where(x => x.Name.Equals(info.Name, StringComparison.InvariantCultureIgnoreCase));

            // Specialized exists.
            if (await specializeds.AnyAsync())
                return ResponseMessage(
                    Request.CreateErrorResponse(HttpStatusCode.Conflict, HttpMessages.CannotBeDuplicated));

            var specialized = new Database.Models.Entities.Specialized
            {
                Name = info.Name,
                Status = MasterItemStatus.Active
            };

            specialized = UnitOfWork.RepositorySpecialized.Insert(specialized);

            await UnitOfWork.CommitAsync();

            return Ok(specialized);
        }

        /// <summary>
        /// Update specialized
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
        public async Task<IHttpActionResult> EditSpecialized([FromUri] int id, [FromBody] EditSpecializedViewModel info)
        {
            #region Parameter validation

            if (info == null)
            {
                info = new EditSpecializedViewModel();
                Validate(info);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #endregion

            #region Find specialized

            var specializeds = UnitOfWork.RepositorySpecialized.Search();

            var specialized = await specializeds.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (specialized == null)
            {
                return ResponseMessage(
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.SpecializedNotFound));
            }

            #endregion

            #region Update specialized information

            // Check whether information has been updated or not.
            var bHasInformationChanged = false;

            // Name is specified.
            if (info.Name != null && info.Name != specialized.Name)
            {
                specialized.Name = info.Name;
                bHasInformationChanged = true;
            }

            // Status is defined.
            if (info.Status != null && info.Status != specialized.Status)
            {
                specialized.Status = info.Status.Value;
                bHasInformationChanged = true;
            }

            // Information has been changed. Update the date time.
            if (bHasInformationChanged)
            {
                // Save information into database.
                await UnitOfWork.CommitAsync();
            }

            #endregion

            return Ok(specialized);
        }

        /// <summary>
        /// Search for a list of specialized
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [Route("load-specialized")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> LoadSpecialized(SearchSpecializedViewModel info)
        {
            #region Parameter validation

            if (info == null)
            {
                info = new SearchSpecializedViewModel();
                Validate(info);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #endregion

            // Get all specialized from database
            var specializeds = UnitOfWork.RepositorySpecialized.Search();

            // Id have been defined
            if (info.Ids != null && info.Ids.Count > 0)
            {
                info.Ids = info.Ids.Where(x => x > 0).ToList();
                if (info.Ids != null && info.Ids.Count > 0)
                {
                    specializeds = specializeds.Where(x => info.Ids.Contains(x.Id));
                }
            }

            // Name have been defined
            if (info.Names != null && info.Names.Count > 0)
            {
                info.Names = info.Names.Where(x => !string.IsNullOrEmpty(x)).ToList();
                if (info.Names.Count > 0)
                    specializeds = specializeds.Where(x => info.Names.Any(y => x.Name.Contains(y)));
            }

            // Statuses have been defined.
            if (info.Statuses != null && info.Statuses.Count > 0)
            {
                info.Statuses =
                    info.Statuses.Where(x => Enum.IsDefined(typeof(MasterItemStatus), x)).ToList();
                if (info.Statuses.Count > 0)
                    specializeds = specializeds.Where(x => info.Statuses.Contains(x.Status));
            }

            // Do sorting.
            var sorting = info.Sort;
            if (sorting != null)
                specializeds = UnitOfWork.Sort(specializeds, sorting.Direction, sorting.Property);
            else
                specializeds = UnitOfWork.Sort(specializeds, SortDirection.Ascending,
                    SpecializedPropertySort.Name);

            // Paginate.
            var result = new SearchResult<IList<Database.Models.Entities.Specialized>>
            {
                Total = specializeds.Count(),
                Records = await UnitOfWork.Paginate(specializeds, info.Pagination).ToListAsync()
            };

            return Ok(result);
        }

        #endregion

    }
}