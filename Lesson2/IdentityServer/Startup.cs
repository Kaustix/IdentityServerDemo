using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

// Identity Server Home Page: http://localhost:5000
// Identity Sever Configuration Page: http://localhost:5000/.well-known/openid-configuration

// Run Migrations
// dotnet ef migrations add InitialIdentityServerPersistedGrantDbMigration -c PersistedGrantDbContext -o Data/Migrations/IdentityServer/PersistedGrantDb
// dotnet ef migrations add InitialIdentityServerConfigurationDbMigration -c ConfigurationDbContext -o Data/Migrations/IdentityServer/ConfigurationDb

namespace IdentityServer
{
    public class Startup
    {
        public static IConfiguration Configuration { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            //const string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;database=IdentityServer;trusted_connection=yes;";
            //var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddMvc();

            services
                .AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddTestUsers(Config.GetUsers().ToList())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryApiResources(Config.GetApiResources());

            //.AddConfigurationStore(options =>
            //{
            //    options.ConfigureDbContext = builder =>
            //        builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
            //})
            //.AddOperationalStore(options =>
            //{
            //    options.ConfigureDbContext = builder =>
            //        builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly));
            //    options.EnableTokenCleanup = true;
            //    options.TokenCleanupInterval = 30;
            //});
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            //InitializeDatabase(app);

            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

                var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

                //context.Database.EnsureDeleted();
                context.Database.Migrate();
                

                // Seed Clients
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                // Seed Identity Resources
                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                // Seed Api Resources
                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}