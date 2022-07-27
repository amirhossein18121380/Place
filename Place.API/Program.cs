using Place.API.ExtensionMethods;

var builder = WebApplication.CreateBuilder(args);
CustomExtensionMethods.ConfigureServices(builder.Services, builder.Configuration);
var app = builder.Build();
CustomExtensionMethods.Configure(app, app.Environment, app.Configuration);
app.Run();
