using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using ApiMultiPartFormData;
using Newtonsoft.Json.Serialization;

namespace StudentManagement.Configs
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                "DefaultApi",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            config.Formatters.Add(new ApiMultipartFormDataFormatter());
        }
    }
}