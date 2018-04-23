# do IT Publish/Subscribe
A strongly typed publish/subscribe utility levaraging .NET Core's DI framework for subscribe/unsubscribe.

## Quick start
Create a class by implementing the interface `IMessage`.

    class SampleMessage : IMessage
    {
        public string Id { get; set; }
        public byte[] Data { get; set; }
    }

Create one or more message handler for handling messages of this type by implementing the interface `IMessageHandler<T>`.

    class SampleMessageHandler1 : IMessageHandler<SampleMessage>
    {
        public async Task HandleAsync(SampleMessage msg)
        {
            // Do whatever...
        }
    }

    class SampleMessageHander2 : IMessageHandler<SampleMessage>
    {
        public async Task HandleAsync(SampleMessage msg)
        {
            // Do something else...
        }
    }

Subscribe to messages of the `SampleMessage` type simply by registering the classes `SampleMessageHandler1` and `SampleMessageHandler2` with .NET Core's standard dependency injection framework, typically in your `Startup.cs`file.

    public void ConfigureServices(IServiceCollection services)
    {
        // Register the publish/subscribe message hub.
        services.AddPublishSubscribe();
        
        services.AddTransient<IMessageHandler<SampleMessage>, SampleMessageHandler1>();
        services.AddTransient<IMessageHandler<SampleMessage>, SampleMessageHandler2>();
    }

Since we use .NET Core's DI framework for everything, our message handlers can get any registered services passed to their constructors (or whichever injection strategy you like).

Finally, you can invoke the message handlers simply by publishing a `SampleMessage` using `IMessageHub` (an implementation of you get from .NET Core DI framework).

    public HomeController : Controller
    {
        private readonly IMessageHub _msgHub;

        public HomeController(IMessageHub msgHub)
        {
            _msgHub = msgHub;
        }

        public IActionResult Get()
        {
            // Publish a message.
            _msgHub.Publish(new SampleMessage { Id = "123", Data = /* Some data */ });

            // Do something more...
        }
    }
