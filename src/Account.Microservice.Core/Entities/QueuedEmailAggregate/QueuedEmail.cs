using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Account.Microservice.SharedKernel.Interfaces;
using Account.Microservice.SharedKernel;
using System.Xml.Linq;
using Account.Microservice.Core.Constants;

namespace Account.Microservice.Core.Entities.QueuedEmailAggregate;
public class QueuedEmail : EntityBase, IAggregateRoot
{
  #region Ctor
  public QueuedEmail(string from, string fromName, string to, string subject, string body, bool isBodyHtml)
  {
    From = from;
    FromName = fromName;
    To = to;
    Subject = subject;
    Body = body;
    IsBodyHtml = isBodyHtml;
    CreateDate = DateTime.UtcNow;
  }
  #endregion

  #region Propertises
  /// <summary>
  /// Gets or sets the priority
  /// </summary>
  public QueuedEmailPriority PriorityId { get; set; }

  public QueuedEmailStatus Status { get; set; }

  /// <summary>
  /// Gets or sets the From property (email address)
  /// </summary>
  public string From { get; set; }

  /// <summary>
  /// Gets or sets the FromName property
  /// </summary>
  public string FromName { get; set; }

  /// <summary>
  /// Gets or sets the To property (email address)
  /// </summary>
  public string To { get; set; }

  /// <summary>
  /// Gets or sets the ToName property
  /// </summary>
  public string? ToName { get; set; }

  /// <summary>
  /// Gets or sets the ReplyTo property (email address)
  /// </summary>
  public string? ReplyTo { get; set; }

  /// <summary>
  /// Gets or sets the ReplyToName property
  /// </summary>
  public string? ReplyToName { get; set; }

  /// <summary>
  /// Gets or sets the CC
  /// </summary>
  public string? CC { get; set; }

  /// <summary>
  /// Gets or sets the BCC
  /// </summary>
  public string? Bcc { get; set; }

  /// <summary>
  /// Gets or sets the subject
  /// </summary>
  public string Subject { get; set; }

  /// <summary>
  /// Gets or sets the body
  /// </summary>
  public string Body { get; set; }

  /// <summary>
  ///  Gets or sets the IsBodyHtml
  /// </summary>
  public bool IsBodyHtml { get; set; }

  /// <summary>
  /// Gets or sets the attachment file path (full file path)
  /// </summary>
  public string? AttachmentFilePath { get; set; }

  /// <summary>
  /// Gets or sets the attachment file name. If specified, then this file name will be sent to a recipient. Otherwise, "AttachmentFilePath" name will be used.
  /// </summary>
  public string? AttachmentFileName { get; set; }

  /// <summary>
  /// Gets or sets the download identifier of attached file
  /// </summary>
  public int AttachedDownloadId { get; set; }

  /// <summary>
  /// Gets or sets the date and time in UTC before which this email should not be sent
  /// </summary>
  public DateTime? DontSendBeforeDateUtc { get; set; }

  /// <summary>
  /// Gets or sets the send tries
  /// </summary>
  public int SentTries { get; set; } = 0;

  /// <summary>
  /// Gets or sets the sent date and time
  /// </summary>
  public DateTime? SentOnUtc { get; set; }

  ///// <summary>
  ///// Gets or sets the used email account identifier
  ///// </summary>
  //public int EmailAccountId { get; set; }

  public DateTime CreateDate { get; set; }

  public void SetSent()
  {
    //TODO refactor to Sent
    Status = QueuedEmailStatus.Sent;
    SentOnUtc = DateTime.UtcNow;
  }
  #endregion
}
