using Autofac;
using Account.Microservice.Core.Entities.MediaAggregate;
using Account.Microservice.Core.Interfaces;
using Account.Microservice.Core.Services;
using Account.Microservice.Core.Services.Settings;

namespace Account.Microservice.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<ToDoItemSearchService>()
        .As<IToDoItemSearchService>().InstancePerLifetimeScope();
    //builder.RegisterType<SettingService>()
    //    .As<ISettingService>().InstancePerLifetimeScope();
  }
}
