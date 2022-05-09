using System;
using Basket.Core.Model;

using Xunit;
using FluentAssertions;

namespace Basket.Core.Tests.Model;

public class CustomerBasketModelTests
{
    [Fact]
    public void CustomerBasket_Create_Should_Return_CustomerBasket_With_CustomerId_And_Items()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());
        var items = new[] { new Item(new ProductId(1), 1) };

        // Act
        var customerBasket = CustomerBasket.Create(customerId, items);

        // Assert
        customerBasket.CustomerId.Should().Be(customerId);
        customerBasket.Items.Should().NotBeEmpty();
        customerBasket.Items.Should().HaveCount(1);
        customerBasket.Items[0].ProductId.Should().Be(new ProductId(1));
        customerBasket.Items[0].Quantity.Should().Be(1);
    }

    [Fact]
    public void CustomerBasket_CreateEmpty_Should_Return_CustomerBasket_With_CustomerId_And_EmptyItems()
    {
        // Arrange
        var customerId = new CustomerId(Guid.NewGuid());

        // Act
        var customerBasket = CustomerBasket.Empty(customerId);

        // Assert
        customerBasket.CustomerId.Should().Be(customerId);
        customerBasket.Items.Should().BeEmpty();
        customerBasket.Items.Should().HaveCount(0);
    }
}