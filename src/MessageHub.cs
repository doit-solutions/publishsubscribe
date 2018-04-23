using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DoIt.PublishSubscribe
{
    public class MessageHub : IMessageHub
    {
        private readonly IServiceProvider _services;

        public MessageHub(IServiceProvider services)
        {
            _services = services;
        }

        public async Task PublishAsync<T>(T message) where T : IMessage
        {
            IEnumerable<IMessageHandler<T>> handlers = _services.GetService<IEnumerable<IMessageHandler<T>>>();
            if (handlers != null)
            {
                foreach (IMessageHandler<T> handler in handlers)
                {
                    await handler.HandleAsync(message);
                }
            }
        }
    }
}