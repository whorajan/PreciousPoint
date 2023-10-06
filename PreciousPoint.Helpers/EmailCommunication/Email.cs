using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace PreciousPoint.Helpers.EmailCommunication
{
  public class Email
  {
    public  async Task<bool> SendEmail(EmailViewModel email)
    {
      using(SmtpClient client = new SmtpClient(email.EmailSender.Host))
      {
        client.Port = email.EmailSender.Port;
        client.Credentials = new NetworkCredential(email.EmailSender.UserName, email.EmailSender.Password);
        client.EnableSsl = email.EmailSender.EnableSsl;
        MailMessage mailMessage = new MailMessage()
        {
          From = email.EmailSender.FromAddress,
          Subject = email.Subject,
          Body = email.Body + Environment.NewLine + email.Signature,
          IsBodyHtml = email.IsBodyHtml,
        };
        foreach(var to in email.ToAddress)
        {
          mailMessage.To.Add(to);
        }
        try
        {
          await client.SendMailAsync(mailMessage);
          return true;
        }
        catch(Exception ex)
        {
          Console.WriteLine(ex.Message);
          return false;
        }

      }
    }

    public EmailViewModel GetVerificationEmail(MailAddress userEmail, string userName ,string confirmationLink, IConfiguration configuration)
    {
      EmailViewModel email = new EmailViewModel()
      {
        Body = @"<!DOCTYPE html>
<html>
  <head>
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
    <style>
      body {
        width: 80%;
        margin-left: 10%;
      }
      .card {
        box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2);
        transition: 0.3s;
        width: 100%;
        border-radius: 5px;
        padding: 20px;
      }

      .card:hover {
        box-shadow: 0 8px 16px 0 rgba(0, 0, 0, 0.2);
      }

      .container {
        padding: 2px 16px;
      }

      .button {
        background-color: #4caf50; /* Green */
        border: none;
        color: white;
        padding: 16px 32px;
        text-align: center;
        text-decoration: none;
        display: inline-block;
        font-size: 16px;
        margin: 4px 2px;
        transition-duration: 0.4s;
        cursor: pointer;
      }
      .button1 {
        background-color: white;
        color: black;
        border: 2px solid #4caf50;
      }

      .button1:hover {
        background-color: #4caf50;
        color: white;
      }
    </style>
  </head>
  <body>
    <div class=""card"">
      <h2>Thank you for regestring with us</h2>
      <div class=""container"">
        <h4><b>Dear Mr./Ms.  " + userName +   @" !</b></h4>
        <p>
          Thank you for choosing PreciousPoint! We are delighted to have you
          join our community of valued users. Your registration is almost
          complete, and we just need one more step to ensure your account's
          security and to keep you updated with the latest information. To
          activate your PreciousPoint account, please click on the confirmation
          link below:
        </p>
        <a class=""button button1"" href="" " + confirmationLink + @""" >Click Here</a>
        <p>
          By confirming your email address, you will gain access to all the
          exciting features and benefits that PreciousPoint has to offer. This
          includes updates on our latest products, exclusive offers, and
          valuable resources related to your interests. We want to assure you
          that your information is secure with us. We are committed to
          safeguarding your privacy and ensuring the confidentiality of your
          data. If you encounter any issues or have questions, please feel free
          to contact our support team at [support@email.com]. We're here to
          assist you.
        </p>
        <p>
          Once again, thank you for choosing PreciousPoint. We look forward to
          providing you with a valuable and rewarding experience.
        </p>
        <p>
          Best regards,<br />
          Rajan Kr. Soni<br />
          PreciousPoint
        </p>
      </div>
    </div>
  </body>
</html>
",
        Signature = @"",
        IsBodyHtml = true,
        Subject  = @"Successfully registered with PreciousPoint. Activation Link",
        EmailSender = new EmailSender()
        {
          EmailSetupName = "Regestration",
          EnableSsl = Convert.ToBoolean(configuration["emailConfigurations:registration_email:enableSsl"] ?? "false"),
          FromAddress = new MailAddress(configuration["emailConfigurations:registration_email:fromAddress"]?? ""),
          Host = configuration["emailConfigurations:registration_email:host"] ??"",
          Port = Convert.ToInt32(configuration["emailConfigurations:registration_email:port"]),
          UserName = configuration["emailConfigurations:registration_email:userName"] ??"",
          Password = configuration["emailConfigurations:registration_email:password"]?? "",
        },
      };
      email.ToAddress.Add(userEmail);

      return email;
    }
  }
}