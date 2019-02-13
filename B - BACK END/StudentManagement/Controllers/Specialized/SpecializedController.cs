using System.Threading.Tasks;
using System.Web.Http;
using Business.Interfaces.Businesses;
using Business.ViewModels.Specialized;
using Shared.Enumerations;
using SharedService.Interfaces.Repositories;
using SharedService.Interfaces.Services;
using StudentManagement.Attributes;

namespace StudentManagement.Controllers.Specialized
{
    [RoutePrefix("api/specialized")]
    public class SpecializedController : ApiBasicController
    {
        #region Properties

        // Instance specialized business
        private readonly ISpecializedBusiness _specializedBusiness;

        #endregion

        #region Constructor

        /// <summary>
        /// Initiate controller with injectors.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="identityService"></param>
        /// <param name="systemTimeService"></param>
        /// <param name="specializedBusiness"></param>
        public SpecializedController(IUnitOfWork unitOfWork,
            IIdentityService identityService,
            ISystemTimeService systemTimeService,
            ISpecializedBusiness specializedBusiness) : base(unitOfWork, identityService, systemTimeService)
        {
            _specializedBusiness = specializedBusiness;
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
        public virtual async Task<IHttpActionResult> CreateSpecialized([FromBody] AddSpecializedViewModel info)
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

            // Call business to add specialized
            var specialized = await _specializedBusiness.AddSpecializedAsync(info);

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

            #region Update specialized

            var specialized = await _specializedBusiness.EditSpecializedAsync(id, info);

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

            // Search for a list of specialized
            var result = await _specializedBusiness.LoadSpecializedAsync(info);

            return Ok(result);
        }

        #endregion

    }
}