using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Database.Enumerations;
using Shared.Resources;
using StudentManagement.Interfaces.Repositories;
using StudentManagement.Interfaces.Services;
using StudentManagement.Models;
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
        public async Task<IHttpActionResult> Login([FromBody] LoginViewModel info)
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

            var accounts = UnitOfWork.RepositoryStudent.Search();

            accounts = accounts.Where(x =>
                x.Username.Equals(info.Username) &&
                x.Password.ToLower() == hashedPassword &&
                x.Status == MasterItemStatus.Active);

            // Find account availability.
            var account = await accounts.FirstOrDefaultAsync();
            if (account == null)
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Forbidden,
                    HttpMessages.AccountNotFound));

            #region Token initialization
            

            // Initiate claim.
            var generic = new Generic(account);

            var claims = new Dictionary<string, string>
            {
                {nameof(account.Id), account.Id.ToString()},
                {nameof(account.Username), account.Username},
                {nameof(account.Fullname), account.Fullname}
            };

            var token = new TokenViewModel();
            token.Code = IdentityService.EncodeJwt(claims, IdentityService.JwtSecret);
            token.Expiration = SystemTimeService.DateTimeUtcToUnix(DateTime.Now.AddSeconds(IdentityService.JwtLifeTime));
            token.LifeTime = IdentityService.JwtLifeTime;

            #endregion

            return Ok(token);

            #endregion
        }

        /// <summary>
        /// Register new account.
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        public async Task<IHttpActionResult> Register(RegisterViewModel info)
        {
            if (info == null)
            {
                info = new RegisterViewModel();
                Validate(info);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok();
        }
        #endregion

    }
}