using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using trialpro.Services;
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
                    .AddJsonFile("Config/smtpConfig.json")
                    .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();


            //Jwt Authentication and Configuration
            var jwtConfig = Configuration.GetSection("Jwt").Get<JwtConfig>();

            services.AddSingleton<ISessionKeyManager>(new JWTSessionKeyManager(jwtConfig));

            ConfigureBindings(services);


            services.AddAuthentication().AddJwtAuthenticationWithKeyAndIssuer(jwtConfig.Key, jwtConfig.Issuer);
        }


        private void ConfigureBindings(IServiceCollection services) 
        {
            services.AddSingleton<IConnectionProvider>(
                new ConnectionProvider(Configuration.GetSection("DatabaseSettings").Get<DBConnectionConfig>())
                );

            //Email
            services.AddSingleton(Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
            services.AddSingleton<IEmailService, MailkitEmailService>();


            services.AddSingleton<Login>();
            services.AddSingleton<Logup>();
            services.AddSingleton<ResetUser>();
            services.AddSingleton<RequestOtp>();

            services.AddSingleton<SentMail>();
            services.AddSingleton<IUserProcessor, UserProcessor>();
            services.AddSingleton<IUserProvider, UserProvider>();
            services.AddSingleton<ITokenProvider, TokenProvider>();

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
