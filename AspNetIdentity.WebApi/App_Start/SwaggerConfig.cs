using System.Web.Http;
using WebActivatorEx;
using AspNetIdentity.WebApi;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace AspNetIdentity.WebApi
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;
            GlobalConfiguration.Configuration
              .EnableSwagger(c => c.SingleApiVersion("v1", "The Church Of Pentecost CMS"))
              .EnableSwaggerUi();
        }
    }
}
