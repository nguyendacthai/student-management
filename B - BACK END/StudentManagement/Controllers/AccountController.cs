using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Database.Enumerations;
using Shared.Resources;
using StudentManagement.Interfaces.Repositories;
using StudentManagement.Interfaces.Services;
using StudentManagement.Models.Account;
using StudentManagement.ViewModels.Account;

namespace StudentManagement.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : ApiBasicController
    {
        #region Constructor

        /// <summary>
        /// Initialize controller with injectors.
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="identityService"></param>
        /// <param name="systemTimeService"></param>
        /// <param name="encryptionService"></param>
        public AccountController(IUnitOfWork unitOfWork, IIdentityService identityService, ISystemTimeService systemTimeService, IEncryptionService encryptionService) : base(unitOfWork, identityService, systemTimeService)
        {
            _encryptionService = encryptionService;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Service which is for encrypting information.
        /// </summary>
        private readonly IEncryptionService _encryptionService;

        #endregion

        #region Methods

        /// <summary>
        /// Login into system.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IHttpActionResult Login([FromBody] LoginViewModel info)
        {
            #region Parameters validation

            if (info == null)
            {
                info = new LoginViewModel();
                Validate(info);
            }

            #endregion

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #region Find account information in database

            // Hash the password first.
            var hashedPassword = _encryptionService.InitMd5(info.Password).ToLower();

            // Find accounts from db
            var accounts = UnitOfWork.RepositoryStudent.Search();

            accounts = accounts.Where(x =>
                x.Username.Equals(info.Username, StringComparison.InvariantCultureIgnoreCase) &&
                x.Status == MasterItemStatus.Active);

            //            // Find account availability.
            //            var account = await accounts.FirstOrDefaultAsync();
            //            if (account == null)
            //                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound,
            //                    HttpMessages.AccountNotFound));

            // Find roles related to user.
            var userRoles = UnitOfWork.RepositoryUserRole.Search();

            var userRolesPairs = (from user in accounts
                                  from userRole in userRoles
                                  where userRole.StudentId == user.Id
                                  select new
                                  {
                                      User = user,
                                      UserRole = userRole
                                  }).ToList();

            var profile = new LoginModel
            {
                User = userRolesPairs.Select(x => x.User).FirstOrDefault(),
                Roles = userRolesPairs.Select(x => x.UserRole.RoleId).ToList()
            };

            // User is not found in database.
            if (profile.User == null)
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    HttpMessages.AccountNotFound));

            // Check user role
            if (profile.Roles == null || profile.Roles.Count < 1)
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden,
                    HttpMessages.NoRoleAssignedToUser));

            // Check Password
            if (!hashedPassword.Equals(profile.User.Password, StringComparison.InvariantCultureIgnoreCase))
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound,
                    HttpMessages.AccountNotFound));

            #region Token initialization

            // Initiate claim.
            //var generic = new Generic(account);

            var claims = new Dictionary<string, string>
            {
                {nameof(profile.User.Id), profile.User.Id.ToString()},
                {nameof(profile.User.Username), profile.User.Username},
                {nameof(profile.User.Fullname), profile.User.Fullname}
            };

            var token = new TokenViewModel();
            token.Code = IdentityService.EncodeJwt(claims, IdentityService.JwtSecret);
            token.Expiration = SystemTimeService.DateTimeUtcToUnix(DateTime.Now.AddSeconds(IdentityService.JwtLifeTime));
            token.LifeTime = IdentityService.JwtLifeTime;

            #endregion

            return Ok(token);

            #endregion
        }

        #endregion

    }
}