using FakeItEasy;
using Xunit;
using Microsoft.Extensions.DependencyInjection;

namespace DoIt.PublishSubscribe.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void MessageHub_Implementation_Has_Transient_Lifetime()
        {
            IServiceCollection services = A.Fake<IServiceCollection>();
            ServiceCollectionExtensions.AddPublishSubscribe(services);
            A.CallTo(() => services.Add(A<ServiceDescriptor>.That.Matches(sd =>
                sd.ServiceType == typeof(IMessageHub) &&
                sd.ImplementationType == typeof(MessageHub) &&
                sd.Lifetime == ServiceLifetime.Transient
            ))).MustHaveHappened();
        }
    }
}