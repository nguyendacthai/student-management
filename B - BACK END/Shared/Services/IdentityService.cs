using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Database.Models.Entities;
using Shared.Interfaces.Services;

namespace Shared.Services
{
    public class IdentityService : IIdentityService
    {
        #region Properties

        /// <summary>
        ///     Find secret key.
        /// </summary>
        public string JwtSecret => ConfigurationManager.AppSettings["jwt.secret"];

        /// <summary>
        ///     Jwt life time (in seconds)
        /// </summary>
        public int JwtLifeTime => int.Parse(ConfigurationManager.AppSettings["jwt.token.lifeTime"]);

        /// <summary>
        ///     Jwt name
        /// </summary>
        public string JwtName => ConfigurationManager.AppSettings["jwt.token.name"];

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        ///     Encode jwt using specific conditions.
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public string EncodeJwt(Dictionary<string, string> claims, string secret)
        {
            var bytes = Encoding.UTF8.GetBytes(secret);
            return JWT.Encode(claims, bytes, JwsAlgorithm.HS256);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Decode jwt using specific conditions.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jwt"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public T DecodeJwt<T>(string jwt, string secret)
        {
            var bytes = Encoding.UTF8.GetBytes(secret);
            return JWT.Decode<T>(jwt, bytes);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Find user profile from http request message.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public ProfileViewModel FindRequestIdentity(HttpRequestMessage request)
        {
            if (!request.Properties.ContainsKey(ClaimTypes.Actor))
                return null;
            return (ProfileViewModel)request.Properties[ClaimTypes.Actor];
        }

        /// <inheritdoc />
        /// <summary>
        ///     Set profile to request message.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="profile"></param>
        public void InitRequestIdentity(HttpRequestMessage request, Student profile)
        {
            if (request.Properties.ContainsKey(ClaimTypes.Actor))
            {
                request.Properties[ClaimTypes.Actor] = profile;
                return;
            }

            request.Properties.Add(ClaimTypes.Actor, profile);
        }

        /// <summary>
        ///     Hash original password to md5-format.
        /// </summary>
        /// <param name="originalPassword"></param>
        /// <returns></returns>
        public string HashPassword(string originalPassword)
        {
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(originalPassword);

            var hash = md5.ComputeHash(inputBytes);
            var sb = new StringBuilder();

            foreach (var t in hash)
                sb.Append(t.ToString("X2"));

            return sb.ToString();
        }

        #endregion
    }
}