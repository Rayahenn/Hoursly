using System.Windows;

namespace Hoursly.Infrastructure.Messages
{
    public class SystemMessageSender : ISystemMessageSender
    {
        public void Send(string message)
        {
            MessageBox.Show(message);
        }
    }
}