

namespace Account.Microservice.Web.CronJobServices;

public class CronJobSendEmail : CronJobService
{
  //private readonly IQueuedEmailLocalService _queuedEmailLocalService;
  public CronJobSendEmail(IScheduleConfig<CronJobSendEmail> config)
      : base(config.CronExpression, config.TimeZoneInfo)
  {
    //_queuedEmailLocalService = queuedEmailLocalService;
  }

  public override Task StartAsync(CancellationToken cancellationToken)
  {
    return base.StartAsync(cancellationToken);
  }

  public override Task DoWork(CancellationToken cancellationToken)
  {
    //TODO call send email
   // _queuedEmailLocalService.ProcessSendQueuedEmail().ConfigureAwait(false);
    return Task.CompletedTask;
  }

  public override Task StopAsync(CancellationToken cancellationToken)
  {
    return base.StopAsync(cancellationToken);
  }
}
