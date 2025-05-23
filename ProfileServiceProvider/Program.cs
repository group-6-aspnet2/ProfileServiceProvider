using Azure.Messaging.ServiceBus;
using Business.Interfaces;
using Business.Services;
using Data.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddScoped<IProfileService, ProfileService>();


builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

//Tagit hjälp av chatgpt 
builder.Services.AddSingleton<ServiceBusClient>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    return new ServiceBusClient(config["ServiceBus:ConnectionString"]);
});

builder.Services.AddScoped<IProfileServiceBusListener, ProfileServiceBusListener>();

var app = builder.Build();
app.MapOpenApi();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ventixe ProfileServiceProvider API");
    options.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var listener = scope.ServiceProvider.GetRequiredService<IProfileServiceBusListener>();
    _ = listener.StartProcessingAsync(); // fire-and-forget
}

app.Run();
