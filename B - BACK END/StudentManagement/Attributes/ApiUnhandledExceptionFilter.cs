using System.Data.Entity.Validation;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using log4net;

namespace StudentManagement.Attributes
{
    public class ApiUnhandledExceptionFilter : IExceptionFilter
    {
        #region Constructors

        /// <summary>
        ///     Default constructor.
        /// </summary>
        public ApiUnhandledExceptionFilter()
        {
            _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        /// <summary>
        ///     Execute exception filter to log 'em all.
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task ExecuteExceptionFilterAsync(HttpActionExecutedContext actionExecutedContext,
            CancellationToken cancellationToken)
        {
            if (actionExecutedContext == null)
                return Task.FromResult(0);

            if (actionExecutedContext.Exception == null)
                return Task.FromResult(0);

            var exception = actionExecutedContext.Exception;

            if (exception is DbEntityValidationException)
            {
                var dbEntityValidationException = (DbEntityValidationException)exception;
                _log.Error(dbEntityValidationException);
                return Task.FromResult(1);
            }

            _log.Error(exception.Message, exception);

            return Task.FromResult(1);
        }

        #endregion

        #region Properties

        /// <inheritdoc />
        /// <summary>
        ///     Allow multiple filter to run or not.
        /// </summary>
        public bool AllowMultiple => true;

        /// <summary>
        ///     Log module.
        /// </summary>
        private readonly ILog _log;

        #endregion
    }
}