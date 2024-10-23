using StorageSystem.EventBus.Events;

namespace StorageSystem.EventBus.Abstractions
{
    public interface IEventBus
    {
        Task PublishAsync(IntegrationEvent @event);
    }
}
