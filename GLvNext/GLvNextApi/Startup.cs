using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using GLvNextApi.Models;
using Microsoft.EntityFrameworkCore;

namespace GLvNextApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<OfferContext>(opt => opt.UseInMemoryDatabase("Offers"));
            //services.AddDbContext<OfferContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("Offers")));
            services.AddControllers();
            services.AddHttpClient("remoteOffers", c =>
            {
                c.BaseAddress = new Uri("https://2d976b39.azurewebsites.net/v1/");
                c.DefaultRequestHeaders.Add("Gl-ApiKey", "8B5A92FC-CA0F-4920-9358-50D2EE74A716");
                c.DefaultRequestHeaders.Add("Gl-AppKey", "FB");
            });
            //could add more types of remote offer worker here also, or have a pool of them
            services.AddHostedService<RemoteOfferWorker>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
