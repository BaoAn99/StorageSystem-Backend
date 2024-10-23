using StorageSystem.EventBus.Events;

namespace StorageSystem.EventBus.Abstractions
{
    public interface IIntegrationEventHandler
    {
        Task Handle(IntegrationEvent @event);
    }

    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
        Task IIntegrationEventHandler.Handle(IntegrationEvent @event) => Handle((TIntegrationEvent)@event);
    }
}
