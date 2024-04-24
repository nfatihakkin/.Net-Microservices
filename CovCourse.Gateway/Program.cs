using CovCourse.Gateway.DelegateHandlers;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile($"configuration.{builder.Environment.EnvironmentName.ToLower()}.json");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient<TokenExchangeDelegateHandler>();
builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_gateway";
    options.RequireHttpsMetadata = false;
});



builder.Services.AddOcelot().AddDelegatingHandler<TokenExchangeDelegateHandler>();
var app = builder.Build();
app.UseAuthorization();
await app.UseOcelot();
app.MapControllers();
app.Run();
