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
using WAMServer.Interfaces.Services;
using WAMServer.Interfaces.Services.Weather;
using WAMServer.Services.Weather;

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
            ConfigureServices(builder.Services, builder.Configuration);
            ConfigureControllers(builder.Services);
            Configure(builder);
            var app = builder.Build();
            InitializeDatabase(app);
            ConfigureMiddleware(app);
            app.Run();
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services of the web application builder.</param>
        /// <param name="configuration">The configuration of the web application.</param>
        private void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            SetupCors(services);
            ConfigureJwtAuthentication(services, configuration);
            RegisterDependencies(services);
        }

        /// <summary>
        /// Configures the controllers.
        /// </summary>
        /// <param name="services">The services of the web application builder.</param>
        private void ConfigureControllers(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });
        }

        /// <summary>
        /// Configures JWT authentication.
        /// </summary>
        /// <param name="services">The services of the web application builder.</param>
        /// <param name="configuration">The configuration of the web application.</param>
        private void ConfigureJwtAuthentication(IServiceCollection services, IConfiguration configuration)
        {
            var jwtIssuer = configuration["Jwt:Issuer"];
            var jwtKey = configuration["Jwt:Key"];
            
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
        }

        /// <summary>
        /// Registers application dependencies.
        /// </summary>
        /// <param name="services">The services of the web application builder.</param>
        private void RegisterDependencies(IServiceCollection services)
        {
            // Add HttpClient
            services.AddHttpClient();

            services.AddTransient<IRepository<User>, DbUserRepository>();
            services.AddTransient<ILoginService, DBLoginService>();
            services.AddTransient<IRepository<Address>, DbAddressRepository>();
            services.AddTransient<IEmailService, DefaultEmailService>();
            services.AddTransient<IRepository<GroundWaterLog>, DbGroundWaterLogRepository>();
            services.AddTransient<IRepository<ControlPC>, DbControlPCRepository>();
            services.AddTransient<IRepository<UserSetting>, DbUserSettingRepository>();
            services.AddTransient<IRepository<ActionLog>, DbActionLogRepository>();
            services.AddTransient<IRepository<ActionType>, DbActionTypeRepository>();
            services.AddTransient<IGroundWaterForecastService, GroundWaterForecastService>();
            services.AddTransient<IWindService, WindService>();
        }

        /// <summary>
        /// Sets up CORS.
        /// </summary>
        /// <param name="services">The services of the web application builder.</param>
        private void SetupCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:3000", "http://projd.renswens.nl")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="builder">The web application builder.</param>
        private void Configure(WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<WamDBContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
        }

        /// <summary>
        /// Initializes the database.
        /// </summary>
        /// <param name="app">The web application.</param>
        private void InitializeDatabase(WebApplication app)
        {
            try
            {
                DBInitializer.Seed(app);
            }
            catch (Exception ex)
            {
                var logger = app.Services.GetRequiredService<ILogger<Startup>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        /// <summary>
        /// Configures the middleware.
        /// </summary>
        /// <param name="app">The web application.</param>
        private void ConfigureMiddleware(WebApplication app)
        {
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.Urls.Add("http://*:5000");
            app.MapControllers();
        }
    }
}