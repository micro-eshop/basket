using Basket.Core.Actors;

using StronglyTypedIds;

namespace Basket.Core.Model;

[StronglyTypedId(converters: StronglyTypedIdConverter.SystemTextJson, backingType: StronglyTypedIdBackingType.Int)]
public partial struct ProductId { }

[StronglyTypedId(converters: StronglyTypedIdConverter.SystemTextJson)]
public partial struct CustomerId { }

public readonly struct Item: IComparable<Item>, IEquatable<Item>
{
    public ProductId ProductId { get; }
    public int Quantity { get; }

    public Item(ProductId productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }

    public Item Add(int quantity)
    {
        return new Item(ProductId, Quantity + quantity);
    }

    public int CompareTo(Item other)
    {
        return ProductId.CompareTo(other.ProductId);
    }

    public bool Equals(Item other)
    {
        return ProductId.Equals(other.ProductId);
    }
}

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


    public CustomerBasket Apply(object @event)
    {
        switch (@event)
        {
            case AddItem addItem:
                return this;
        }
        return this;
    }
    
}