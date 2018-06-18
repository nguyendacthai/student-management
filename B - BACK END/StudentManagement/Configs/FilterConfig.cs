using System.Web.Http.Filters;
using StudentManagement.Attributes;

namespace StudentManagement.Configs
{
    public class FilterConfig
    {
        #region Methods

        /// <summary>
        ///     Register filters to application
        /// </summary>
        /// <param name="httpFilterCollection"></param>
        public static void Register(HttpFilterCollection httpFilterCollection)
        {
            httpFilterCollection.Add(new ApiUnhandledExceptionFilter());
            httpFilterCollection.Add(new BearerAuthenticationFilter());
            //httpFilterCollection.Add(new ApiAuthorizeAttribute());
        }

        #endregion
    }
}