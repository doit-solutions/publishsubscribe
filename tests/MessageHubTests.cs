using FakeItEasy;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace DoIt.PublishSubscribe.Tests
{
    public class TestMessage : IMessage
    {
        public int Id { get; set; }
    }

    public class MessageHubTests
    {
        [Fact]
        public async Task All_Registered_Message_Handler_Services_Are_Invoked_For_Each_Published_Message()
        {
            IMessageHandler<TestMessage> testMessageHandler1 = A.Fake<IMessageHandler<TestMessage>>();
            IMessageHandler<TestMessage> testMessageHandler2 = A.Fake<IMessageHandler<TestMessage>>();
            IServiceProvider services = A.Fake<IServiceProvider>();
            A.CallTo(() => services.GetService(typeof(IEnumerable<IMessageHandler<TestMessage>>))).Returns(new IMessageHandler<TestMessage>[] { testMessageHandler1, testMessageHandler2 });
            IMessageHub hub = new MessageHub(services);
            await hub.PublishAsync(new TestMessage { Id = 123 });
            
            A.CallTo(() => testMessageHandler1.HandleAsync(A<TestMessage>.That.Matches(msg => msg.Id == 123))).MustHaveHappenedOnceExactly();
            A.CallTo(() => testMessageHandler2.HandleAsync(A<TestMessage>.That.Matches(msg => msg.Id == 123))).MustHaveHappenedOnceExactly();
        }
    }
}