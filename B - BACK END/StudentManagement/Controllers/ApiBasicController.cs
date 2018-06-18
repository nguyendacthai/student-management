using System.Web.Http;
using StudentManagement.Interfaces.Repositories;
using StudentManagement.Interfaces.Services;

namespace StudentManagement.Controllers
{
    /// <summary>
    /// Base class which should be inherited by api controllers.
    /// </summary>
    public class ApiBasicController : ApiController
    {
        #region Properties

        /// <summary>
        /// Instance to provide access to database.
        /// </summary>
        protected IUnitOfWork UnitOfWork;

        /// <summary>
        /// Instance to get/set identity.
        /// </summary>
        protected IIdentityService IdentityService;

        /// <summary>
        /// Instance for time calculation.
        /// </summary>
        protected ISystemTimeService SystemTimeService;


        #endregion

        #region Constructors

        /// <summary>
        /// Initialize controller with injectors.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="identityService"></param>
        /// <param name="systemTimeService"></param>
        public ApiBasicController(IUnitOfWork unitOfWork, IIdentityService identityService,
            ISystemTimeService systemTimeService)
        {
            UnitOfWork = unitOfWork;
            IdentityService = identityService;
            SystemTimeService = systemTimeService;
        }

        #endregion
    }
}