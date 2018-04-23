using System;
using System.Threading.Tasks;

namespace DoIt.PublishSubscribe
{
    public interface IMessageHub
    {
        Task PublishAsync<T>(T message) where T : IMessage;
    }
}
