namespace Account.Microservice.Web.CronJobServices;

public interface IScheduleConfig<T>
{
  string CronExpression { get; set; }
  TimeZoneInfo TimeZoneInfo { get; set; }
}
