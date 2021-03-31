using AutoMapper;
using domain_mapping;
using infra_http.Auth.OAuth;
using infra_http.Auth.OAuth.Contracts;
using infra_http.Client;
using infra_http.Client.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using infra_configuration.Clients;
using infra_http.Middleware.Request;
using infra_http.Middleware.Request.Contracts;
using http_infra.Middleware.Response;
using dal_villa.Context;
using Microsoft.EntityFrameworkCore;
using domain_service.Aggregation;
using domain_service.Dashboard;
using domain_constants.Env;
using domain_service.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;

namespace villa_api
{
  public class Startup
  {

    static readonly string WEB_URI = "http://localhost:4000";

    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {

      services.AddControllers();

      services.AddCors(opts =>
      {
        opts.AddDefaultPolicy(builder =>
          {
            // TODO narrow down CORS
            builder.WithOrigins("*");
          });
      });

      services.AddDbContext<VillaContext>(opts =>
      {
        opts.UseNpgsql(Environment.GetEnvironmentVariable(PgEnv.CONN));
      });


      services.AddAutoMapper(typeof(MappingConfiguration));
      services.AddMemoryCache();
      services.AddTransient<AggregationService>();
      services.AddTransient<DashboardService>();
      services.AddTransient<UserService>();

      var cert = new X509Certificate2(@"A:\gdariodev.pem");
      var key = new X509SecurityKey(cert);
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(opts =>
        {

          // opts.Authority  = Configuration["Auht0:Domain"];
          opts.Authority  = @"https://gdariodev.eu.auth0.com/";
          //opts.Audience   = Configuration["Auht0:Audience"];
          opts.Audience   = "pfm.gdario.dev";

          opts.TokenValidationParameters = new TokenValidationParameters
          {
            NameClaimType = ClaimTypes.NameIdentifier,
            IssuerSigningKey = key,
            ValidIssuer = @"https://gdariodev.eu.auth0.com/",
            ValidAudiences = new List<string> 
            { 
"pfm.gdario.dev"
            },
          };
        });

      ConfigureTinkServices(services);

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      // if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

      app.UseMiddleware<ExceptionHandler>();

      app.UseHttpsRedirection();

      // These are ordered -----------------------------------------------------
      // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-5.0#middleware-order
      app.UseRouting();
      app.UseCors( opts => opts.WithOrigins(WEB_URI).AllowAnyMethod().AllowAnyHeader() );
      app.UseAuthentication();
      app.UseAuthorization();
      // -----------------------------------------------------------------------

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });

      /*
      using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
      {
        var ctxt = serviceScope.ServiceProvider.GetRequiredService<VillaContext>();
        ctxt.Database.EnsureCreated();
      }

      */
    }

    void ConfigureTinkServices(IServiceCollection services)
    {

      // TODO move to a client based config @villa-configuration
      services.AddHttpClient<IOAuthClient, TinkOAuthClient>(client =>
      {
        client.BaseAddress = new Uri(@"https://api.tink.com/api/v1/");
      });

      services.AddHttpClient<IAggregationProviderClient, TinkClient>(client =>
      {
        client.BaseAddress = new Uri(@"https://api.tink.com/api/v1/");
      })
      .AddHttpMessageHandler<IExternalOAuthHandler>();

      services.AddTransient<IExternalOAuthHandler, TinkOAuthHandler>();

      services.Configure<TinkSettings>(Configuration.GetSection("TinkSettings"));

    }

  }

}
