
using CitiesAPI;
using CitiesAPI.Services;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;

// this serilog is configured

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/citiesAPI.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();



builder.Host.UseSerilog();

// Add services to the container.


builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
}
).AddNewtonsoftJson()
.AddXmlDataContractSerializerFormatters();
    



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

//For Ilogger
#if DEBUG
builder.Services.AddTransient<IMailServices, LocalMailServices>();
#else
builder.Services.AddTransient<IMailServices, CloudMailService;
#endif

builder.Services.AddSingleton<CitiesDataStore>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//}
//);

app.MapControllers();

app.Run();
