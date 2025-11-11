using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using NLog.Web;
using TaskTracker.Api.FluentValidation;
using TaskTracker.Api.Infrastructure;
using TaskTracker.Api.Middleware;
using TaskTracker.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
       .AddEnvironmentVariables();
// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(fvv=> fvv.RegisterValidatorsFromAssemblyContaining<TaskValidator>());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new()
    {
        Title = "TaskTracker API",
        Version = "v1",
        Description = "API for managing users, tasks and comments."
    });

    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename), true);
});
var connectionString = "Data Source=tasks.db";
DbInitializer.Initialize(connectionString);

builder.Services.AddScoped<ITaskRepository>(sp => new TaskRepository(connectionString));
builder.Services.AddScoped<IUserRepository>(sp => new UserRepository(connectionString));
builder.Services.AddScoped<ICommentRepository>(sp => new CommentRepository(connectionString));


builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task Tracker API v1");
    });
    
}
app.UseMiddleware<GlobalExceptionMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
