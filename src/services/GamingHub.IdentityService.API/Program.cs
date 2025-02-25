using EasyCaching.Core.Configurations;
using EasyCaching.Redis;
using EasyCaching.Serialization.SystemTextJson.Configurations;
using GamingHub.IdentityService.API;
using GamingHub.IdentityService.API.Configuration;
using GamingHub.IdentityService.API.V1.Providers;
using GamingHub.IdentityService.API.V1.Providers.Implementation;
using GamingHub.IdentityService.API.V1.Validators;
using GamingHub.IdentityService.API.V1.Validators.Implementation;
using GamingHub.Service.Shared.Providers;
using GamingHub.Service.Shared.Providers.Implementation;
using GamingHub.UserService.gRPC.V1;

var builder = WebApplication.CreateBuilder(args);

// Add configures to the container.
builder.Services.Configure<RedisCacheOptions>(builder.Configuration.GetSection(RedisCacheOptions.NAME_KEY));
builder.Services.Configure<TwilioOptions>(builder.Configuration.GetSection(TwilioOptions.NAME_KEY));
builder.Services.Configure<UserServiceOptions>(builder.Configuration.GetSection(UserServiceOptions.NAME_KEY));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddIdentityServer()
    .AddDeveloperSigningCredential()
    .AddInMemoryApiScopes(IdentityConfig.ApiScopes)
    .AddInMemoryClients(IdentityConfig.Clients)
    .AddExtensionGrantValidator<PhoneNumberTokenGrantValidator>()
    .AddResourceOwnerValidator<PasswordGrantValidator>();
builder.Services.AddGrpcClient<UserService.UserServiceClient>(o =>
{
    var userServiceOptions = builder.Configuration
        .GetSection(UserServiceOptions.NAME_KEY)
        .Get<UserServiceOptions>() ?? throw new InvalidOperationException($"{nameof(UserServiceOptions)} not found");

    o.Address = new Uri(userServiceOptions.Endpoint);
});

builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddScoped<ITwilioProvider, TwilioProvider>();
builder.Services.AddTransient<IPhoneNumberValidator, TwilioVerifyPhoneNumberValidator>();
builder.Services.AddEasyCaching(options =>
{
    var redisOptions = builder.Configuration
        .GetSection(RedisCacheOptions.NAME_KEY)
        .Get<RedisCacheOptions>() ?? throw new InvalidOperationException($"{nameof(RedisCacheOptions)} not found");

    options.UseRedis(config =>
        {
            var endpoint = redisOptions.Endpoint.Trim().Split(":");
            config.DBConfig = new RedisDBOptions
            {
                Endpoints = { new ServerEndPoint(endpoint[0], int.Parse(endpoint[1])) },
                SyncTimeout = redisOptions.SyncTimeout,
            };
        }, redisOptions.Name)
        .WithSystemTextJson(redisOptions.Name);
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseIdentityServer();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", () => "Identity Service is running...");

app.Run();