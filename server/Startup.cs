using Microsoft.EntityFrameworkCore;

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
        builder.Services.AddControllers();
        configureServices(builder);
        configure(builder);
        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }

    /// <summary>
    /// Configures the services.
    /// </summary>
    /// <param name="builder">The webapplicationbuilder responsible for making the app.</param>
    private void configureServices(WebApplicationBuilder builder)
    {
        // Add services to the container.
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