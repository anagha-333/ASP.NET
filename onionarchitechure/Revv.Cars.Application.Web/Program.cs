using AutoMapper;
using MediatR;
using Microsoft.OpenApi.Models;

using Revv.Cars.DomainService.MongoDb;
using Revv.Cars.DomainService.QueriesHandler;
using MicrosoftLogLevel = Microsoft.Extensions.Logging.LogLevel;

var builder = WebApplication.CreateBuilder(args);

// ✅ Read MongoDB settings from appsettings.json
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));


// ✅ Add Controllers
builder.Services.AddControllers();

// ✅ Swagger Setup
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Revv API", Version = "v1" });
});

// ✅ Authorization (Authentication config assumed to be elsewhere if needed)
builder.Services.AddAuthorization();

// ✅ Register MediatR handlers
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<GetAllCarQueryHandler>());

// ✅ Register AutoMapper profiles
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ✅ Static files (for images etc.)
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthentication();      // Only needed if you’ve configured JWT/Auth
app.UseAuthorization();

app.MapControllers();
app.Run();
