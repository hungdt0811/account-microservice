using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.Core.Interfaces;

namespace Account.Microservice.Core.Entities.SettingAggregate;
public class AwsS3Settings : ISettings
{
  public string? AccessKey { get; set; }
  public string? SecretAccessKey { get; set; }
  public string? BucketRegion { get; set; }
  public string? BucketName { get; set; }
  public string? QueueUrl { get; set; }
  public int MaxNumberOfMessages { get; set; } = 10;
  public int WaitTimeSeconds { get; set; } = 20;
}
