using CandidateHub.Application;
using CandidateHub.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment}.json", optional: true, reloadOnChange: true);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Environment, builder.Configuration);

var app = builder.Build();

app.AddInfrastructureApplication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
