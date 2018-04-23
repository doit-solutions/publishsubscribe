using System.Threading.Tasks;

namespace DoIt.PublishSubscribe
{
    public interface IMessageHandler<T> where T : IMessage
    {
        Task HandleAsync(T message);
    }
}