using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using WebApplication2.Entities;

namespace WebApplication2
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
            // Inject DB context
            services.AddScoped<DbContext>();
            
            // Add OData
            services.AddOData();

            // Add MVC
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowAll")
                .UseMvc(routeBuilder =>
                {
                    // Set up OData
                    routeBuilder.MapODataServiceRoute("odata", string.Empty, GetEdmModel());
                    routeBuilder.EnableDependencyInjection();
                });
        }

        private static IEdmModel GetEdmModel()
        {
            var builder = new ODataConventionModelBuilder();

            builder.EntitySet<Bank>("Banks")
                .EntityType
                .HasKey(o => o.Id)
                .Page(10, 10)
                .Filter(QueryOptionSetting.Allowed);

            builder.EntitySet<Manager>("Managers")
                .EntityType
                .HasKey(o => o.Id)
                .Page(10, 10)
                .Filter(QueryOptionSetting.Allowed);

            return builder.GetEdmModel();
        }
    }
}
