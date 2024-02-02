using System.Configuration;
using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Account.Microservice.Core;
using Account.Microservice.Core.Services.Users;
using Account.Microservice.Infrastructure;
using Account.Microservice.Infrastructure.Data;
using Account.Microservice.Web;
using Account.Microservice.Web.Authorization;
using Account.Microservice.Web.Configuration;
using Account.Microservice.Web.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using FluentValidation.AspNetCore;
using System.Reflection;
using Account.Microservice.Web.CronJobServices;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Account.Microservice.Core.Helpers;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

builder.Services.Configure<CookiePolicyOptions>(options =>
{
  options.CheckConsentNeeded = context => true;
  options.MinimumSameSitePolicy = SameSiteMode.None;
});

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");  //Configuration.GetConnectionString("DefaultConnection");
// configure strongly typed settings object
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

var maxRequest = builder.Configuration.GetSection("MaxRequestBodySize");

builder.Services.AddCors();
//builder.Services.AddDbContext(connectionString);
builder.Services.AddDbContext<AppDbContext>(c =>
                c.UseSqlServer(connectionString));
builder.Services.AddRazorPages();
// Add service
builder.Services.AddCoreServices();
builder.Services.Configure<IISServerOptions>(options =>
{
  options.MaxRequestBodySize = ConvertHelper.AsLong(maxRequest!.Value!, int.MaxValue);
});

builder.Services.Configure<FormOptions>(options =>
{
  // Set the limit to 128 MB
  options.ValueLengthLimit = ConvertHelper.AsInt(maxRequest!.Value!, int.MaxValue);
  options.MultipartBodyLengthLimit = ConvertHelper.AsLong(maxRequest!.Value!, int.MaxValue);
  options.MultipartHeadersLengthLimit = ConvertHelper.AsInt(maxRequest!.Value!, int.MaxValue);
});

builder.Services.Configure<KestrelServerOptions>(options =>
{
  options.Limits.MaxRequestBodySize = ConvertHelper.AsLong(maxRequest!.Value!, int.MaxValue);
});

//AddCoreServices(builder.Services.ad);

// add fluent validation

builder.Services.AddControllers(options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true).
  AddFluentValidation(options =>
{
  // Automatic registration of validators in assembly
  options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
}).AddJsonOptions(options =>
{
  options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddSwaggerGen(c =>
{
  c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
  c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
  {
    In = ParameterLocation.Header,
    Description = "Please enter a valid token",
    Name = "Authorization",
    Type = SecuritySchemeType.Http,
    BearerFormat = "JWT",
    Scheme = "Bearer"
  });
  c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
  c.EnableAnnotations();
});

// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(config =>
{
  config.Services = new List<ServiceDescriptor>(builder.Services);

  // optional - default path to view services is /listallservices - recommended to choose your own path
  config.Path = "/listservices";
});

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterModule(new DefaultCoreModule());
  containerBuilder.RegisterModule(new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
});
// add cron job service
builder.Services.AddCronJob<CronJobSendEmail>(c =>
{
  c.TimeZoneInfo = TimeZoneInfo.Local;
  c.CronExpression = @"* * * * *";
});

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
  app.UseShowAllServicesMiddleware();
}
else
{
  app.UseExceptionHandler("/Home/Error");
  app.UseHsts();
}
app.UseRouting();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();
// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Hiromori API V1"));

app.UseEndpoints(endpoints =>
{
  endpoints.MapDefaultControllerRoute();
  endpoints.MapRazorPages();
});

// Seed Database
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;

  try
  {
    var context = services.GetRequiredService<AppDbContext>();
    //context.Database.Migrate();
    context.Database.EnsureCreated();
    //SeedData.Initialize(services);
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
  }
}

app.Run();
