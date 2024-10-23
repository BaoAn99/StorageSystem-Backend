using Microsoft.Extensions.Logging;
using StorageSystem.EventBus.Abstractions;
using StorageSystem.EventBus.Events;

namespace StorageSystem.Infrastructure.EventBusRabbitMQ
{
    public sealed class RabbitMQEventBus(
        ILogger<RabbitMQEventBus> logger) : IEventBus, IDisposable
    {
        public Task PublishAsync(IntegrationEvent @event)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
