using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using Account.Microservice.Core.Interfaces;
using Humanizer.Localisation;
using Microsoft.Extensions.Logging;

namespace Account.Microservice.Infrastructure;

public class SmtpEmailSender : IEmailSender
{
  private readonly ILogger<SmtpEmailSender> _logger;

  public SmtpEmailSender(ILogger<SmtpEmailSender> logger)
  {
    _logger = logger;
  }

  public async Task SendEmailAsync(string fromEmail,
           string fromName,
           string toEmail,
           string username,
           string password,
           string host,
           int port,
           bool enableSsl,
           string subject,
           string body)
  {
    MailMessage message = new MailMessage();
    message.IsBodyHtml = true;
    message.From = new MailAddress(fromEmail, fromName);
    message.To.Add(new MailAddress(toEmail));
    message.Subject = subject;
    message.Body = body;
    // Comment or delete the next line if you are not using a configuration set
    //message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);

    //using (var client = new System.Net.Mail.SmtpClient(host, port))
    //{
    //  // Pass SMTP credentials
    //  client.Credentials =
    //      new NetworkCredential(username, password);

    //  // Enable SSL encryption
    //  client.EnableSsl = enableSsl;

    //  // Try to send the message. Show status in console.
    //  try
    //  {
    //    _logger.LogDebug("Attempting to send email...");
    //    await client.SendMailAsync(message);
    //    _logger.LogDebug("Email sent!");
    //  }
    //  catch (Exception ex)
    //  {
    //    _logger.LogError("The email was not sent.");
    //    _logger.LogError("Error message: " + ex.Message);
    //  }
    //}
    var resources = BarcodeResources(ref body);
    using (AlternateView altBody = AlternateView.CreateAlternateViewFromString(
      body,
      message.BodyEncoding,
      message.IsBodyHtml ? "text/html" : null))
    {
      altBody.TransferEncoding = TransferEncoding.SevenBit;
      foreach (var resource in resources!)
      {
        altBody.LinkedResources.Add(resource);
      }
      message.AlternateViews.Add(altBody);
      try
      {
        using (var client = new System.Net.Mail.SmtpClient(host, port))
        {
          // Pass SMTP credentials
          client.Credentials =
              new NetworkCredential(username, password);

          // Enable SSL encryption
          client.EnableSsl = enableSsl;

          // Try to send the message. Show status in console.
          try
          {
            _logger.LogDebug("Attempting to send email...");
            await client.SendMailAsync(message);
            _logger.LogDebug("Email sent!");
          }
          catch (Exception ex)
          {
            _logger.LogError("The email was not sent.");
            _logger.LogError("Error message: " + ex.Message);
          }
        }
      }
      catch (SmtpException ex)
      {
        _logger.LogError("The email was not sent.");
        _logger.LogError("Error message: " + ex.Message);
      }
    }
  }

  public List<LinkedResource>? BarcodeResources(ref string htmlSource)
  {
    var result = new List<LinkedResource>();
    string regexImgSrc = @"<img[^>]*?src\s*=\s*[" +
      "']?([^'"
      +
      " >]+?)[ '"
      +
      "][^>]*?>";
    MatchCollection matchesImgSrc = Regex.Matches(htmlSource, regexImgSrc,
    RegexOptions.IgnoreCase | RegexOptions.Singleline);
    foreach (Match m in matchesImgSrc)
    {
      string href = m.Groups[1].Value;
      href = href.Replace(" ", "");
      // only need the base64 encoded images
      if (!href.Contains("base64"))
      {
        continue;
      }
      string base64String = href.Substring(href.IndexOf(",") + 1);
      byte[] filebytes = Convert.FromBase64String(base64String);
      var newResource = new LinkedResource(new MemoryStream(filebytes), "image/png");
      result.Add(newResource);
      htmlSource = htmlSource.Replace(href, string.Format("cid:{0}", newResource.ContentId));
    }
    return result;
  }
}
