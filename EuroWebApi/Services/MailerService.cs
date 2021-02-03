using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Web;
using EuroWebApi.Models;

namespace EuroWebApi.Services
{

    public class MailerService
    {
        #region "Constructor"

        private IDictionary<MessageType, string> _templatePathDictionary = new Dictionary<MessageType, string>(){
            {MessageType.CustomerInvoiceEmail                    , "Services.assets.templates.customer-invoice-email.html"},
        };
        private static MailerService obj;
        private string SMTPServer { get; set; }
        private int Port { get; set; }
        private string EmailUser { get; set; }
        private string EmailFrom { get; set; }
        private string EmailPassword { get; set; }
        private bool EnableSSL { get; set; }
        private SmtpClient _smtpClient;

        public static MailerService Instance()
        {
            if (obj == null)
            {
                obj = new MailerService();
                using (var context = new EURODbContext())
                {
                    var emailConfig = context.AppConfig.Where(f => f.ParentKey == "EMAIL").ToList();
                    try
                    {
                        if (emailConfig != null && emailConfig.Count() > 0)
                        {
                            obj.SMTPServer = Convert.ToString(emailConfig.Find(e => e.ChildKEy == "SMTPServer").ConfigValue);
                            obj.Port = Convert.ToInt16(emailConfig.Find(e => e.ChildKEy == "EMAILPORT").ConfigValue);
                            obj.EmailUser = Convert.ToString(emailConfig.Find(e => e.ChildKEy == "EMAILUSER").ConfigValue);
                            obj.EmailFrom = Convert.ToString(emailConfig.Find(e => e.ChildKEy == "EMAILFROM").ConfigValue);
                            obj.EmailPassword = Convert.ToString(emailConfig.Find(e => e.ChildKEy == "EMAILPASSWORD").ConfigValue);
                            obj.EnableSSL = Convert.ToBoolean(Convert.ToInt16(emailConfig.Find(e => e.ChildKEy == "ISSSLENABLED").ConfigValue));
                        }
                    }
                    catch (Exception)
                    {
                        obj = null;
                    }
                }
            }
            return obj;
        }

        private MailerService() { }

        #endregion


        public ResponseModel Send(Message message)
        {
            ResponseModel response = null;
            try
            {
                _SetupSmtpClient();

                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(this.EmailFrom),
                    IsBodyHtml = true,
                    Subject = message.Subject
                };

                mailMessage.To.Add(message.To);
                //mailMessage.Bcc.Add(this.EmailFrom);
                if (message.Attachment != null)
                {
                    mailMessage.Attachments.Add(new Attachment(new MemoryStream(message.Attachment), message.AttachmentName));
                }
                else if (!string.IsNullOrEmpty(message.AttachmentPath))
                {
                    Attachment attach = new Attachment(message.AttachmentPath);
                    attach.Name = message.AttachmentName;
                    mailMessage.Attachments.Add(attach);
                }

                mailMessage.AlternateViews.Add(_InitiliazeEmailView(message.Type, message.Data));

                _smtpClient.Send(mailMessage);
                response = new ResponseModel { IsError = false, Message = "A email with your current password has been sent, please check." };
            }
            catch (Exception ex)
            {
                response = new ResponseModel { IsError = true, Message = "Email Sending Fail Due to following error: " + ex.Message };
            }
            return response;
        }

        private void _SetupSmtpClient()
        {
            _smtpClient = new SmtpClient(this.SMTPServer, this.Port)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(this.EmailUser, this.EmailPassword)
            };
        }

        private AlternateView _InitiliazeEmailView(MessageType type, Dictionary<string, string> data)
        {
            Assembly _currentAssembly = Assembly.GetExecutingAssembly();

            LinkedResource logoResource =
                new LinkedResource(_currentAssembly.GetManifestResourceStream($"{_currentAssembly.GetName().Name}.Services.assets.imgs.logo_transparent.gif"))
                {
                    ContentId = "Logo"
                };

            StreamReader reader = new StreamReader(_currentAssembly.GetManifestResourceStream($"{_currentAssembly.GetName().Name}.{_templatePathDictionary[type]}"));
            StringBuilder htmlTemplate = new StringBuilder(reader.ReadToEnd());
            foreach (var replacement in data)
            {
                htmlTemplate.Replace(replacement.Key, replacement.Value);
            }
            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlTemplate.ToString(), null, "text/html");
            htmlView.LinkedResources.Add(logoResource);

            return htmlView;
        }
    }

    public class Message
    {
        public Message()
        {
            Data = new Dictionary<string, string>();
        }
        public string To { get; set; }
        public string Subject { get; set; }
        public MessageType Type { get; set; }
        public Dictionary<string, string> Data { get; set; }
        public string AttachmentName { get; set; }
        public byte[] Attachment { get; set; }
        public string AttachmentPath { get; set; }
    }

    public enum MessageType
    {
        CustomerInvoiceEmail,
    }


    public class ResponseModel
    {
        public bool IsError { get; set; }

        public string Message { get; set; }
    }
}