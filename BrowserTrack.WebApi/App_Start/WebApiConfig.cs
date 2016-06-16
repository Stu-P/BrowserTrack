using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using BrowserTrack.WebApi.App_Start;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;

namespace BrowserTrack.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            //enables cross origin resource
            //  config.EnableCors();


            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var json = config.Formatters.JsonFormatter;
            json.SerializerSettings.PreserveReferencesHandling = Newtonsoft.Json.PreserveReferencesHandling.Objects;

           // config.Formatters.Remove(config.Formatters.XmlFormatter);

            var jsonStyleFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().FirstOrDefault();
            jsonStyleFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

        }
    }
}
