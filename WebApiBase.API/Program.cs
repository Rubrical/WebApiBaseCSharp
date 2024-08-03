using WebApiBase.Infrastructure.IOC;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);
startup.RegisterServices(builder.Services, builder.Configuration);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();

startup.Configure(app, app.Environment);
app.Run();
