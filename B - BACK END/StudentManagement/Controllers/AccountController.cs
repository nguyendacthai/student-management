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
using Shared.Resources;
using SharedService.Interfaces.Repositories;
using SharedService.Interfaces.Services;
using SharedService.ViewModels;
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
        /// <param name="emailService"></param>
        /// <param name="profileCacheService"></param>
        public AccountController(IUnitOfWork unitOfWork, IIdentityService identityService, ISystemTimeService systemTimeService, IEncryptionService encryptionService, IValueCacheService<int, ProfileViewModel> profileCacheService, IEmailService emailService) : base(unitOfWork, identityService, systemTimeService)
        {
            _encryptionService = encryptionService;
            _emailService = emailService;
            _profileCacheService = profileCacheService;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Service which is for encrypting information.
        /// </summary>
        private readonly IEncryptionService _encryptionService;

        /// <summary>
        ///     Service which is for sending email.
        /// </summary>
        private readonly IEmailService _emailService;

        /// <summary>
        /// Service which is for caching profile information.
        /// </summary>
        private readonly IValueCacheService<int, ProfileViewModel> _profileCacheService;

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

            // Convert user information to profile.
            var cachedProfile = AutoMapper.Mapper.Map<Database.Models.Entities.Student, ProfileViewModel>(profile.User);
            cachedProfile.Roles = profile.Roles;

            // Push information back to cache.
            _profileCacheService.Add(cachedProfile.Id, cachedProfile);

            #endregion

            return Ok(token);

            #endregion
        }

        /// <summary>
        ///     Get user profile attached with request (parsed by authenticatoin attribute)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("personal-profile")]
        public IHttpActionResult FindPersonalProfile()
        {
            // Find profile attached with request.
            var profile = IdentityService.FindRequestIdentity(Request);
            return Ok(profile);
        }

        /// <summary>
        /// Register new account
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Register([FromBody] AccountRegisterViewModel info)
        {
            #region Parameter validation

            if (info == null)
            {
                info = new AccountRegisterViewModel();
                Validate(info);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #endregion

            var students = UnitOfWork.RepositoryStudent.Search();
            students =
                students.Where(x => x.Username.Equals(info.Username, StringComparison.InvariantCultureIgnoreCase) || x.Email.Equals(info.Email, StringComparison.InvariantCultureIgnoreCase));

            // Student exists.
            if (await students.AnyAsync())
                return ResponseMessage(
                    Request.CreateErrorResponse(HttpStatusCode.Conflict, HttpMessages.CannotBeDuplicated));

            var student = new Database.Models.Entities.Student()
            {
                Username = info.Username,
                Password = IdentityService.HashPassword(info.Password),
                Fullname = info.Fullname,
                Gender = info.Gender,
                Email = info.Email,
                Phone = info.Phone,
                ForgotPassword = false,
                Status = MasterItemStatus.Active
            };

            student = UnitOfWork.RepositoryStudent.Insert(student);

            var userRole = new Database.Models.Entities.UserRole
            {
                StudentId = student.Id,
                RoleId = (int)UserRoles.Normal
            };

            userRole = UnitOfWork.RepositoryUserRole.Insert(userRole);

            //await UnitOfWork.CommitAsync();

            return Ok();
        }

        /// <summary>
        /// Forgot password
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [Route("forgot-password")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IHttpActionResult> ForgotPassword([FromBody] ForgotPasswordViewModel info)
        {
            #region Parameter validation

            if (info == null)
            {
                info = new ForgotPasswordViewModel();
                Validate(info);
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            #endregion

            var students = UnitOfWork.RepositoryStudent.Search();
            var account =
               await students.Where(x => x.Email.Equals(info.Email, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefaultAsync();

            // Check account exist or not
            if (account == null)
                return ResponseMessage(
                    Request.CreateErrorResponse(HttpStatusCode.NotFound, HttpMessages.AccountNotFound));

            // Update account
            account.ForgotPassword = true;

            await UnitOfWork.CommitAsync();

            //            var mailTo = new MailAddress("datptitcntt@gmail.com");
            //            _emailService.SendMail(new[]{ mailTo },"Hi", "Hello Dat",null, false, false);

            return Ok(account);
        }

        #endregion

    }
}