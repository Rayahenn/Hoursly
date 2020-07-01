using System;

namespace Hoursly.Infrastructure.Emails
{
    public interface IEmailSender
    {
        void Send(string supervisorEmail, DateTime startDate, DateTime endDate);
    }
}