using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Microservice.Core.Constants;
public static class MessageTemplateSystemNames
{
  public const string SendMailResetPassword = "System.SendMailResetPassword";
  public const string SendMailCreateUser = "System.SendMailCreateUser";
  public const string SendMailConfirmShipping = "System.SendMailConfirmShipping";
  public const string SendMailInquiry = "System.SendMailInquiry";
  public const string SendMailConfirmShippingQrCode = "System.SendMailConfirmShippingQrCode";
}
