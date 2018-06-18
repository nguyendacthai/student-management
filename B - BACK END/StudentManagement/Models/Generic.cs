using System.Collections.Generic;
using System.Security.Claims;
using Database.Models.Entities;

namespace StudentManagement.Models
{
    public class Generic
    {
        #region Properties

        /// <summary>
        /// Username of account.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Fullname
        /// </summary>
        public string Fullname { get; set; }

        /// <summary>
        /// Phone of account.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Time when token should be expired.
        /// </summary>
        public double ExpirationTime { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initiate generic token using account information.
        /// </summary>
        /// <param name="account"></param>
        public Generic(Student account)
        {
            Username = account.Username;
            Fullname = account.Fullname;
            Phone = account.Phone;
        }

        #endregion

        //#region Methods

        ///// <summary>
        ///// Get claim from object.
        ///// </summary>
        ///// <returns></returns>
        //public Dictionary<string, string> ToClaims()
        //{
        //    var claims = new Dictionary<string, string>();
        //    claims.Add(ClaimTypes.Email, Email);
        //    claims.Add(ClaimTypes.Uri, Photo);
        //    claims.Add(ClaimTypes.Role, $"{Role}");
        //    claims.Add(ClaimTypes.Expiration, ExpirationTime.ToString("N"));
        //    return claims;
        //}

        //#endregion
    }
}