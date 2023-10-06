using System.Net.Mail;

namespace PreciousPoint.Helpers.EmailCommunication
{
#pragma warning disable CS8618
  public class EmailSender
  {
    public string EmailSetupName { get; set; }

    public string Host { get; set; }

    public int Port { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public MailAddress FromAddress { get; set; }

    public bool EnableSsl { get; set; } = true;

  }
#pragma warning restore CS8618
}