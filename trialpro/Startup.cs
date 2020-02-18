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
using trialpro.Models;
using trialpro.Controllers;
using trialpro.Services;
using System.Data;
using trialpro.Tasks;

namespace trialpro
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(env.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .AddJsonFile("Config/appsettings.database.json", optional: true)
                    .AddJsonFile("Config/jwtconfig.json", optional: true)
                    .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            var dbConnConfig = new DBConnectionConfig();
            Configuration.Bind("DatabaseSettings", dbConnConfig);

            services.AddSingleton(dbConnConfig);

            var connProvider = new ConnectionProvider(dbConnConfig);

            var jwtConfig = new JwtConfig();
            Configuration.Bind("Jwt", jwtConfig);

            services.AddSingleton(jwtConfig);

            services.AddSingleton<IConnectionProvider>(connProvider);
            services.AddSingleton(connProvider.GetConnection());

            services.AddSingleton<IUserProcessor, UserProcessor>();
            services.AddSingleton<IUserProvider, UserProvider>();
            services.AddSingleton<ITokenProvider, TokenProvider>();
            services.AddSingleton<ISessionKeyManager, JWTSessionKeyManager>();
            services.AddSingleton<RequestOtp>();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
