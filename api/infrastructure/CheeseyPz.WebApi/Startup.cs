using Application.UseCases.Configuration;
using CheeseyPz.WebApi.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CheeseyPz.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string CORSPolicy = nameof(CORSPolicy);

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            services.AddControllers();
            services.AddAndConfigureServices(Configuration);
            services.AddAndConfigureApplicationServices(Configuration);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CheeseyPz.WebApi", Version = "v1" });
            });

            services.AddCors(c =>
            {
                c.AddPolicy(CORSPolicy, options => options
                    .WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CheeseyPz.WebApi v1"));
            //}

            app.UseRouting();
            app.UseCors(CORSPolicy);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


            //app.UseSpa(spa =>
            //{
            //    if (env.IsLocal())
            //    {
            //        spa.UseProxyToSpaDevelopmentServer(Configuration.GetValue<string>("SpaLiveDevelopmentProxyUrl"));
            //    }
            //});
            //app.UseSession();
            //app.UseMvc();
        }
    }
}
