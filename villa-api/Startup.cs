using AutoMapper;
using domain_mapping;
using http_infra.Client;
using http_infra.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using villa_configuration.Clients;

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
      services.AddHttpClient<IAggregationProviderClient, TinkClient>(client =>
      {
        // TODO move to a client based config @villa-configuration
        client.BaseAddress = new Uri(@"https://api.tink.com/api/v1/");
      });

      services.Configure<TinkSettings>(Configuration.GetSection("TinkSettings"));

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
  }
}
