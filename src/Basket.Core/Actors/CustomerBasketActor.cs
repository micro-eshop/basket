using Basket.Core.Model;

using Proto;
using Proto.Persistence;
using Proto.Persistence.SnapshotStrategies;

namespace Basket.Core.Actors;
internal record AddItem(ProductId ProductId, int Quantity);

public class CustomerBasketActor : IActor
{
    private CustomerBasket _state = CustomerBasket.Empty(CustomerId.Empty);
    private readonly Persistence _persistence;
    private bool _timerStarted;
    private int _snapshot;


    public CustomerBasketActor(CustomerId customerId, IProvider provider)
    {
        _state = CustomerBasket.Empty(customerId);
        _persistence = Persistence.WithEventSourcingAndSnapshotting(
        provider,
        provider,
        $"customer-basket-{customerId.Value}",
        evt => ApplyEvent(evt),
        evt => ApplySnapshot(evt),
        new IntervalStrategy(20), () => _state);
    }

    public Task ReceiveAsync(IContext context)
    {
        var msg = context.Message;

        return Task.CompletedTask;
    }

    private void ApplyEvent(Event @event)
    {
        switch (@event)
        {
            case RecoverEvent msg:
                _state.Apply(msg.Data);
                break;
            case ReplayEvent msg:
                _state.Apply(msg.Data); 
                break;
            case PersistedEvent msg:
                Console.WriteLine("MyPersistenceActor - PersistedEvent = Event.Index = {0}, Event.Data = {1}", msg.Index, msg.Data);
                break;
        }
    }

    private void ApplySnapshot(Snapshot snapshot)
    {
        switch (snapshot)
        {
            case RecoverSnapshot msg:
                if (msg.State is CustomerBasket basket)
                {
                    _state = basket;
                }
                break;
        }
    }
}