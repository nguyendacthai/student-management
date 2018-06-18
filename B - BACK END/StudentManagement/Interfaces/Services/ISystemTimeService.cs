using System;

namespace StudentManagement.Interfaces.Services
{
    public interface ISystemTimeService
    {
        #region Methods

        /// <summary>
        ///     Convert UTC date time to unix.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        double DateTimeUtcToUnix(DateTime dateTime);

        /// <summary>
        ///     Convert unix date time to utc date time.
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        DateTime UnixToDateTimeUtc(double unixTime);

        #endregion
    }
}