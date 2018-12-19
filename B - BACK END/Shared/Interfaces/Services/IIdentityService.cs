using System.Collections.Generic;
using System.Net.Http;
using Database.Models.Entities;

namespace Shared.Interfaces.Services
{
    public interface IIdentityService
    {
        #region Properties

        /// <summary>
        ///     Find secret key.
        /// </summary>
        string JwtSecret { get; }

        /// <summary>
        ///     Jwt life time (in seconds)
        /// </summary>
        int JwtLifeTime { get; }

        /// <summary>
        ///     Jwt name
        /// </summary>
        string JwtName { get; }

        #endregion

        #region Methods

        /// <summary>
        ///     Encode jwt from claims and secret.
        /// </summary>
        /// <param name="claims"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        string EncodeJwt(Dictionary<string, string> claims, string secret);

        /// <summary>
        ///     Decode jwt using secret.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jwt"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        T DecodeJwt<T>(string jwt, string secret);

        /// <summary>
        ///     Find  request identity information.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        ProfileViewModel FindRequestIdentity(HttpRequestMessage request);

        /// <summary>
        ///     Set profile to request message.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="profile"></param>
        void InitRequestIdentity(HttpRequestMessage request, Student profile);

        /// <summary>
        ///     Hash original password.
        /// </summary>
        /// <param name="originalPassword"></param>
        /// <returns></returns>
        string HashPassword(string originalPassword);

        #endregion
    }
}