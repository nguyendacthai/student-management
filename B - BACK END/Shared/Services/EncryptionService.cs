using System.Security.Cryptography;
using System.Text;
using Shared.Interfaces.Services;

namespace Shared.Services
{
    public class EncryptionService : IEncryptionService
    {
        #region Methods

        /// <summary>
        ///     Initiate md5 hash.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string InitMd5(string text)
        {
            // Use input string to calculate MD5 hash
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.ASCII.GetBytes(text);
                var hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                var sb = new StringBuilder();
                for (var i = 0; i < hashBytes.Length; i++)
                    sb.Append(hashBytes[i].ToString("X2"));
                return sb.ToString();
            }
        }

        #endregion
    }
}