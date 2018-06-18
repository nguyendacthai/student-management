using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using log4net;
using StudentManagement.Interfaces.Services;
using StudentManagement.Services;

namespace StudentManagement.Attributes
{
    public class BearerAuthenticationFilter : IAuthenticationFilter
    {
        #region Constructors

        /// <summary>
        ///     Initiate middleware instance with default logging.
        /// </summary>
        public BearerAuthenticationFilter()
        {
            Log = LogManager.GetLogger(typeof(BearerAuthenticationFilter));
            IdentityService = new IdentityService();
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Whether multiple authentication is supported or not.
        /// </summary>
        public bool AllowMultiple => false;

        /// <summary>
        ///     Instance which serves logging process of log4net.
        /// </summary>
        public ILog Log { get; set; }

        /// <summary>
        ///     Identity service.
        /// </summary>
        public IIdentityService IdentityService { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        ///     Authenticate a request asynchronously.
        /// </summary>
        /// <param name="httpAuthenticationContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task AuthenticateAsync(HttpAuthenticationContext httpAuthenticationContext,
            CancellationToken cancellationToken)
        {
            // Account has been authenticated before token is parsed.
            // Skip the authentication.
            var principal = httpAuthenticationContext.Principal;
            if (principal != null && principal.Identity != null && principal.Identity.IsAuthenticated)
                return Task.FromResult(0);

            // FullSearch the authorization in the header.
            var authorization = httpAuthenticationContext.Request.Headers.Authorization;

            // Bearer token is detected.
            if (authorization == null)
                return Task.FromResult(0);

            // Scheme is not bearer.
            if (!"Bearer".Equals(authorization.Scheme,
                StringComparison.InvariantCultureIgnoreCase))
                return Task.FromResult(0);

            // Token parameter is not defined.
            var token = authorization.Parameter;
            if (string.IsNullOrWhiteSpace(token))
                return Task.FromResult(0);

            try
            {
                // FullSearch authentication provider from request sent from client.
                // Decode the token and set to claim. The object should be in dictionary.
                var claimPairs = IdentityService.DecodeJwt<Dictionary<string, string>>(token,
                    IdentityService.JwtSecret);

                var claimIdentity = new ClaimsIdentity(null, IdentityService.JwtName);
                foreach (var key in claimPairs.Keys)
                {
                    if (string.IsNullOrEmpty(claimPairs[key]))
                        continue;

                    claimIdentity.AddClaim(new Claim(key, claimPairs[key]));
                }
                // Authenticate the request.
                httpAuthenticationContext.Principal = new ClaimsPrincipal(claimIdentity);
            }
            catch (Exception exception)
            {
                // Suppress error.
                Log.Error(exception.Message, exception);
            }

            return Task.FromResult(0);
        }

        /// <summary>
        ///     Callback which is called after the authentication which to handle the result.
        /// </summary>
        /// <param name="httpAuthenticationChallengeContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ChallengeAsync(HttpAuthenticationChallengeContext httpAuthenticationChallengeContext,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(0);
        }

        #endregion
    }
}