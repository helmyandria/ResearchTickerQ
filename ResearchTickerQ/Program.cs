using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;
using TickerQ.Dashboard.DependencyInjection;
using TickerQ.DependencyInjection;

namespace ResearchTickerQ
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTickerQ(opt =>
            {
                opt.AddDashboard(dbopt =>
                {
                    //Mount path for the dashboard UI(default: "/tickerq-dashboard").
                   dbopt.BasePath = "/tickerq-dashboard";

                   // Allowed CORS origins for dashboard API (default: ["*"]).
                   //dbopt.CorsOrigins = new[] { "https://arcenox.com" };

                    // Backend API domain (scheme/SSL prefix supported).
                    //dbopt.BackendDomain = "ssl:arcenox.com";

                    // Authentication
                    //dbopt.EnableBuiltInAuth = true;  // Use TickerQ’s built-in auth (default).
                    //dbopt.UseHostAuthentication = false; // Use host auth instead (off by default).
                    //dbopt.RequiredRoles = new[] { "Admin", "Ops" };
                    //dbopt.RequiredPolicies = new[] { "TickerQDashboardAccess" };

                    // Basic auth toggle (default: false).
                    //dbopt.EnableBasicAuth = true;

                    // Pipeline hooks
                    //dbopt.PreDashboardMiddleware = app => { /* e.g., request logging */ };
                    //dbopt.CustomMiddleware = app => { /* e.g., extra auth/rate limits */ };
                    //dbopt.PostDashboardMiddleware = app => { /* e.g., metrics collection */ };
                });

            });

            var app = builder.Build();

            app.UseTickerQ(); // Activates job processor

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
