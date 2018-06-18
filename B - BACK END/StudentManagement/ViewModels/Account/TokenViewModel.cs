namespace StudentManagement.ViewModels.Account
{
    public class TokenViewModel
    {
        #region Properties

        /// <summary>
        ///     Code which is for accessing into service api.
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     Life time of token
        /// </summary>
        public int LifeTime { get; set; }

        /// <summary>
        ///     When token is expired (UTC)
        /// </summary>
        public double Expiration { get; set; }

        #endregion
    }
}