using DependencyInjection.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Register the same class with different lifetimes using different interfaces
builder.Services.AddSingleton<IRandomNumberService, RandomNumberService>();
//builder.Services.AddScoped<IRandomNumberService, RandomNumberService>();
//builder.Services.AddTransient<IRandomNumberService, RandomNumberService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
