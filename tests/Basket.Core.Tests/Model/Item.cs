using Basket.Core.Model;

using Xunit;
using FluentAssertions;

namespace Basket.Core.Tests.Model;

public class ItemModelTests
{
    [Theory]
    [InlineData(1, 1, 2)]
    [InlineData(2, 2, 4)]
    [InlineData(3, 3, 6)]
    [InlineData(4, 1, 5)]
    public void Item_Add_Should_Return_Item_With_Quantity(int quantity, int quantityToAdd, int expectedQuantity)
    {
        // Arrange
        var item = new Item(new ProductId(1), quantity);

        // Act
        var result = item.Add(quantityToAdd);

        // Assert
        result.Quantity.Should().Be(expectedQuantity);

    }
}