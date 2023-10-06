using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace PreciousPoint.Helpers.EmailCommunication
{
#pragma warning disable CS8618
  public class EmailViewModel
  {
    [EmailAddress]
    public IList<MailAddress> ToAddress { get; set; } = new List<MailAddress>();

    [EmailAddress]
    public IList<MailAddress> CcAddress { get; set; } = new List<MailAddress>();

    [EmailAddress]
    public ICollection<MailAddress> BccAddress { get; set; } = new List<MailAddress>();

    public string[] Attachment { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

    public string Signature { get; set; }

    public bool IsBodyHtml { get; set; } = false;

    public EmailSender EmailSender { get; set; }
    
  }
#pragma warning restore CS8618
}