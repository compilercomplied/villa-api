using AutoMapper;
using domain_mapping;
using infra_http.Auth.OAuth;
using infra_http.Auth.OAuth.Contracts;
using infra_http.Client;
using infra_http.Client.Contracts;
using infra_http.Middleware;
using infra_http.Middleware.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using infra_configuration.Clients;

namespace villa_api
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }


    public void ConfigureServices(IServiceCollection services)
    {

      services.AddControllers();
      services.AddAutoMapper(typeof(MappingConfiguration));
      services.AddHttpClient();
      services.AddMemoryCache();

      ConfigureTinkServices(services);

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
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
