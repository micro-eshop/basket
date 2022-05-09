using StronglyTypedIds;

namespace Basket.Core.Model;

[StronglyTypedId(converters: StronglyTypedIdConverter.SystemTextJson, backingType: StronglyTypedIdBackingType.Int)]
public partial struct ProductId { }

[StronglyTypedId(converters: StronglyTypedIdConverter.SystemTextJson)]
public partial struct CustomerId { }

public readonly record struct Item(ProductId ProductId, int Quantity);

public sealed class CustomerBasket
{
    public CustomerId CustomerId { get; init; }
    public IReadOnlyList<Item> Items { get; init; }

    private CustomerBasket(CustomerId customerId, IReadOnlyList<Item> items)
    {
        CustomerId = customerId;
        Items = items;
    }

    public static CustomerBasket Create(CustomerId customerId, IReadOnlyList<Item> items)
    {
        return new CustomerBasket(customerId, items);
    }

    public static CustomerBasket Empty(CustomerId id)
    {
        return new CustomerBasket(id, Array.Empty<Item>());
    }
}