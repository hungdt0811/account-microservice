namespace Account.Microservice.Web.CronJobServices;

public class ScheduleConfig<T> : IScheduleConfig<T>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
  public string CronExpression { get; set; }
  public TimeZoneInfo TimeZoneInfo { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
