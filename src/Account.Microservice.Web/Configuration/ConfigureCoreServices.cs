using Account.Microservice.Core.Entities.MediaAggregate;
using Account.Microservice.Core.Entities.SecurityAggregate;
using Account.Microservice.Core.Entities.SettingAggregate;
using Account.Microservice.Core.Helpers;
using Account.Microservice.Core.Interfaces;
using Account.Microservice.Core.Services.Courses;
using Account.Microservice.Core.Services.EmailAccounts;
using Account.Microservice.Core.Services.EmailTemplates;
using Account.Microservice.Core.Services.Medias;
using Account.Microservice.Core.Services.Paginations;
using Account.Microservice.Core.Services.QueuedEmails;
using Account.Microservice.Core.Services.RolePermissions;
using Account.Microservice.Core.Services.Settings;
using Account.Microservice.Core.Services.Users;
using Account.Microservice.Infrastructure;
using Account.Microservice.Web.Authorization;
using Account.Microservice.Web.Helpers;
using Account.Microservice.Web.Services.Course;
using Account.Microservice.Web.Services.EmailAccounts;
using Account.Microservice.Web.Services.Medias;
using Account.Microservice.Web.Services.Role;
using Account.Microservice.Web.Services.Settings;
using Account.Microservice.Web.Services.Stripe;
using Account.Microservice.Web.Services.Users;
using Stripe;

namespace Account.Microservice.Web.Configuration;

public static class ConfigureCoreServices
{
  public static void AddCoreServices(this IServiceCollection services)
  {
    // add services
    services.AddTransient<IUserService, UserService>();
    services.AddScoped<ICodeGeneratorService, CodeGeneratorService>();
    services.AddScoped<IEncryptionService, EncryptionService>();
    services.AddTransient<IEmailAccountService, EmailAccountService>();
    services.AddScoped<IJwtUtils, JwtUtils>();
    services.AddScoped<IEmailTemplateService, EmailTemplateService>();
    services.AddTransient<IQueuedEmailService, QueuedEmailService>();
    services.AddScoped<IEmailSender, SmtpEmailSender>();
    services.AddScoped<ISettingService, SettingService>();
    services.AddScoped<IRolePermissionService, RolePermissionService>();

    services.AddScoped<IMediaService, MediaService>();
    services.AddScoped<IAppFileProvider, AppFileProvider>();
    services.AddScoped<ICourseService, CourseService>();

    services.AddScoped<IPaginationService, PaginationService>();

    services.AddScoped<IMediaLocalService, MediaLocalService>();


    // local service
    services.AddTransient<IEmailRelatedService, EmailRelatedService>();
    services.AddScoped<IEmailRelatedLocalService, EmailRelatedLocalService>();
    services.AddScoped<ISettingLocalService, SettingLocalService>();
    services.AddScoped<ITripeService, TripeService>();
    services.AddScoped<ChargeService>();
    services.AddScoped<TokenService>();
    services.AddScoped<IUserLocalService, UserLocalService>();
    services.AddScoped<IRolePermissionLocalService, RolePermissionLocalService>();
    services.AddScoped<ICourseLocalService, CourseLocalService>();
    //helper
    services.AddScoped<IDateTimeHelper, DateTimeHelper>();
    
    //add factories

    services.AddTransient<IEmailAccountLocalService, EmailAccountLocalService>();

    services.AddScoped(typeof(MediaSettings), serviceProvider =>
    {
      return serviceProvider.GetRequiredService<ISettingService>().LoadSettingAsync(typeof(MediaSettings)).Result;
    });
    services.AddScoped(typeof(CommonSettings), serviceProvider =>
    {
      return serviceProvider.GetRequiredService<ISettingService>().LoadSettingAsync(typeof(CommonSettings)).Result;
    });
    services.AddScoped(typeof(AwsS3Settings), serviceProvider =>
    {
      return serviceProvider.GetRequiredService<ISettingService>().LoadSettingAsync(typeof(AwsS3Settings)).Result;
    });
    services.AddScoped(typeof(DateTimeSettings), serviceProvider =>
    {
      return serviceProvider.GetRequiredService<ISettingService>().LoadSettingAsync(typeof(DateTimeSettings)).Result;
    });
    services.AddScoped(typeof(StripeSettings), serviceProvider =>
    {
      return serviceProvider.GetRequiredService<ISettingService>().LoadSettingAsync(typeof(StripeSettings)).Result;
    });
    services.AddScoped(typeof(EmailAccountSettings), serviceProvider =>
    {
      return serviceProvider.GetRequiredService<ISettingService>().LoadSettingAsync(typeof(EmailAccountSettings)).Result;
    });
  }
}
