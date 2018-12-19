using System;
using SharedService.Interfaces.Services;

namespace SharedService.Services
{
    public class SystemTimeService : ISystemTimeService
    {
        #region Properties

        /// <summary>
        ///     Base UTC.
        /// </summary>
        private readonly DateTime _utcDateTime;

        #endregion

        #region Constructors

        /// <summary>
        ///     Parameterless constructor.
        /// </summary>
        public SystemTimeService()
        {
            _utcDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Calculate the unix time from UTC DateTime.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public double DateTimeUtcToUnix(DateTime dateTime)
        {
            return (dateTime - _utcDateTime).TotalMilliseconds;
        }

        /// <summary>
        ///     Convert unix time to UTC time.
        /// </summary>
        /// <param name="unixTime"></param>
        /// <returns></returns>
        public DateTime UnixToDateTimeUtc(double unixTime)
        {
            return _utcDateTime.AddMilliseconds(unixTime);
        }

        #endregion
    }
}