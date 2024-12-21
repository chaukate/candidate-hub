using CandidateHub.Application.Interfaces;
using CandidateHub.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CandidateHub.Infrastructure
{
    public static class DependencyInjection
    {
        private static IWebHostEnvironment _webHostEnv;
        private static IConfiguration _configuration;

        public static void AddInfrastructure(this IServiceCollection services,
                                             IWebHostEnvironment webHostEnv,
                                             IConfiguration configuration)
        {
            _webHostEnv = webHostEnv;
            _configuration = configuration;

            services.AddSwagger();

            services.AddDbContext<ChDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("CHub"));
            });

            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        }

        private static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = $"Candidate Hub - {_webHostEnv.EnvironmentName}"
                });
                options.CustomSchemaIds(c => c.FullName);
            });
        }

        public static void AddInfrastructureApplication(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Candidate Hub");
                    options.RoutePrefix = "swagger";
                });
            });
        }
    }
}
