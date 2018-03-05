using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

// Identity Server Home Page: http://localhost:5000
// Identity Sever Configuration Page: http://localhost:5000/.well-known/openid-configuration

namespace IdentityServer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}