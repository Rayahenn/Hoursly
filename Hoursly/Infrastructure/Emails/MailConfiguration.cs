using System.Configuration;

namespace Hoursly.Infrastructure.Emails
{
    public class MailConfiguration : ConfigurationSection
    {
        public const string SectionName = nameof(MailConfiguration);


        [ConfigurationProperty("smtpServer", DefaultValue = "")]
        public string SmtpServer
        {
            get => this["smtpServer"].ToString();
            set => this["smtpServer"] = value;
        }

        [ConfigurationProperty("port", DefaultValue = "0")]
        public int Port
        {
            get => (int) this["port"];
            set => this["port"] = value;
        }

        [ConfigurationProperty("fromLogin", DefaultValue = "")]
        public string FromLogin
        {
            get => this["fromLogin"].ToString();
            set => this["fromLogin"] = value;
        }

        [ConfigurationProperty("fromPassword", DefaultValue = "")]
        public string FromPassword
        {
            get => this["fromPassword"].ToString();
            set => this["fromPassword"] = value;
        }

        [ConfigurationProperty("subject", DefaultValue = "")]
        public string Subject
        {
            get => this["subject"].ToString();
            set => this["subject"] = value;
        }

        [ConfigurationProperty("body", DefaultValue = "")]
        public string Body
        {
            get => this["body"].ToString();
            set => this["body"] = value;
        }
    }
}