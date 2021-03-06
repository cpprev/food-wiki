using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Food.Core;
using Authentication;
using Microsoft.AspNetCore.Authentication;

namespace Controllers
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Controllers", Version = "v1" });
            });

            // Put services here!
            services.AddSingleton<FoodService>();

            services.AddControllersWithViews();

            // ApiKey auth setup
            services.AddAuthentication(o => {
                o.DefaultScheme = SchemesNamesConst.TokenAuthenticationDefaultScheme;
            }).AddScheme<AuthenticationSchemeOptions, TokenAuthenticationHandler>(SchemesNamesConst.TokenAuthenticationDefaultScheme, o => { });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Controllers v1"));
            }

            // TODO Authorize the URLS here : for now put * to allow every url
            app.Use((context, next) =>
            {
                context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return next.Invoke();
            });

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
