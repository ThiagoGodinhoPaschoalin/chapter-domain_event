using Newtonsoft.Json.Converters;
using Serilog;
using Serilog.Events;
using SharedDomain;
using WebAppWithEventDelegate.Facade;

#region do not look
var builder = WebApplication.CreateBuilder(args);

LoggerConfiguration loggerConfiguration = new();
loggerConfiguration
    .MinimumLevel.ControlledBy(new Serilog.Core.LoggingLevelSwitch(LogEventLevel.Debug))
    .MinimumLevel.Override("Microsoft", new Serilog.Core.LoggingLevelSwitch(LogEventLevel.Warning))
    .MinimumLevel.Override("System", new Serilog.Core.LoggingLevelSwitch(LogEventLevel.Warning));
loggerConfiguration
    .WriteTo.Console(LogEventLevel.Verbose, "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}");
Log.Logger = loggerConfiguration.CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers().AddNewtonsoftJson(opt => { opt.SerializerSettings.Converters.Add(new StringEnumConverter()); });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGenNewtonsoftSupport();
#endregion do not look

builder.Services.AddSharedDomainDependencyInjection();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<OccurrencyService>();

#region do not look
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
#endregion do not look
