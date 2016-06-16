using BrowserTrack.Data.EntityFramework;
using BrowserTrack.Data.Repositories;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Ninject;
using Ninject.Web.Common;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;

using System.Reflection;
using System.Web.Http;
using BrowserTrack.WebApi.Providers;
using System;
using Swashbuckle.Application;
using System.Net.Http;
using BrowserTrack.WebApi.Services;
using BrowserTrack.WebApi.Models;

[assembly: OwinStartup(typeof(BrowserTrack.WebApi.Startup))]
namespace BrowserTrack.WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            ConfigureOAuth(app);
            HttpConfiguration config = new HttpConfiguration();

            ConfigureSwagger(config);
            CreateMappings();

            WebApiConfig.Register(config);




            //  app.UseWebApi(config);

            app.UseNinjectMiddleware(CreateKernel);
            app.UseNinjectWebApi(config);




        }

        private void CreateMappings()
        {
            AutoMapper.Mapper.Initialize(cfg => {
                cfg.CreateMap<EnquiryRequestModel, MailEnquiry>();
            });

        }

        private void ConfigureSwagger(HttpConfiguration config)
        {
            

            config.EnableSwagger(
                c => {
                    c.SingleApiVersion("v1", "BrowserTrack.WebApi");
                    c.RootUrl(req => new Uri(req.RequestUri, req.GetRequestContext().VirtualPathRoot).ToString());
                })
                    .EnableSwaggerUi();

            
        }

        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = System.TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }


        private static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            kernel.Load(Assembly.GetExecutingAssembly());

            
            return kernel;
        }


        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IBrowserTrackRepository>().To<BrowserTrackRepository>().InRequestScope();
            kernel.Bind<IAuthRepository>().To<AuthRepository>().InRequestScope();

            kernel.Bind<BrowserTrackDBContext>().To<BrowserTrackDBContext>().InRequestScope();
            kernel.Bind<AuthDBContext>().To<AuthDBContext>().InRequestScope();


        }

    }
}