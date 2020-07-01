namespace Hoursly.Infrastructure.Messages
{
    public interface ISystemMessageSender
    {
        void Send(string message);
    }
}