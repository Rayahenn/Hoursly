using System.Windows;

namespace Hoursly.Common.Messages
{
    public class SystemMessageSender : ISystemMessageSender
    {
        public void Send(string message)
        {
            MessageBox.Show(message);
        }
    }
}