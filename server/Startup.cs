using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WAMServer.Interfaces;
using WAMServer.Models;
using WAMServer.Repositories;

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
            app.UseAuthentication();
            app.UseAuthorization();
            app.Urls.Add("http://localhost:5000");
            app.MapControllers();
            app.Run();
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="service">The services of the webappbuilder</param>
        private void configureServices(IServiceCollection services, IConfiguration configuration)
        {
            var jwtIssuer = configuration.GetSection("Jwt:Issuer").Get<string>();
            var jwtKey = configuration.GetSection("Jwt:Key").Get<string>();
            // Add services to the container.
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
            services.AddTransient<IRepository<Address>, DbAddressRepository>();
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