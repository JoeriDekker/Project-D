using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WAMServer.Seeders;
using WAMServer.Interfaces;
using WAMServer.Models;
using WAMServer.Repositories;
using WAMServer.Services;

namespace WAMServer
{
    /// <summary>
    /// The Startup class is the entry point of the application.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Responsible for starting the application.
        /// </summary>
        public void Start()
        {
            var builder = WebApplication.CreateBuilder();
            configureServices(builder.Services, builder.Configuration);
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
            configure(builder);
            var app = builder.Build();
            DBInitializer.Seed(app);
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.Urls.Add("http://*:5000");
            app.MapControllers();
            app.Run();
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="service">The services of the webappbuilder</param>
        private void configureServices(IServiceCollection services, IConfiguration configuration)
        {
            // setup cors
            setupCors(services);
            var jwtIssuer = configuration.GetSection("Jwt:Issuer").Get<string>();
            var jwtKey = configuration.GetSection("Jwt:Key").Get<string>();
            // JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtIssuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey ?? "WamsSuper$ecretKey"))
                    };
                });
            services.AddTransient<IRepository<User>, DbUserRepository>();
            services.AddTransient<ILoginService, DBLoginService>();
            services.AddTransient<IRepository<Address>, DbAddressRepository>();
            services.AddTransient<IEmailService, DefaultEmailService>();
            services.AddTransient<IRepository<WaterLevelSettings>, DbWaterLevelSettingsRepository>();
            services.AddTransient<IRepository<GroundWaterLog>, DbGroundWaterLogRepository>();
            services.AddTransient<IRepository<ControlPC>, DbControlPCRepository>();
            services.AddTransient<IRepository<UserSetting>, DbUserSettingRepository>();
            services.AddTransient<IRepository<ActionLog>, DbActionLogRepository>();
            services.AddTransient<IRepository<ActionType>, DbActionTypeRepository>();
            services.AddTransient<IWaterLevelSettings, DBWaterlevelSettingsService>();
            
        }

        /// <summary>
        /// Sets up the cors. CORS is a security feature that allows you to restrict which domains can access your API.
        /// </summary>
        private void setupCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(["http://localhost:3000", "http://projd.renswens.nl"])
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="builder">The webapplicationbuilder responsible for making the app.</param>
        private void configure(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<WamDBContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        }
    }
}