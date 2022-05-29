using Basket.Core.Model;

using Akka;
using Akka.Actor;

namespace Basket.Core.Actors;
public record AddItem(ProductId ProductId, int Quantity);
public record GetItems();


public class CustomerBasketsActor : ReceiveActor
{
    private CustomerBasket _state = CustomerBasket.Empty(CustomerId.Empty);


    public CustomerBasketsActor(CustomerId customerId)
    {
    }

    public static Props Props(CustomerId customerId) =>
        Akka.Actor.Props.Create(() => new CustomerBasketsActor(customerId));
}