using System;
using DeFuncto.Optics;
using Xunit;

namespace DeFuncto.Tests.Core.Optics;

public class Lens
{
    [Fact]
    public void Should_Lens_Mutate_Get()
    {
        // Arrange
        var box = new Box(new Candy("Snickers", 2), "Yellow");
        var candyLens = new Lens<Box, Candy>(b => b.Candy, (c, b) => new Box(b.Candy = c, b.Color));
        
        // Act
        var candy = candyLens.Get(candyLens, box);
        
        // Assert
        Assert.Equal(box.Candy.Name, candy.Name);
    }
    
    [Fact]
    public void Should_Lens_Mutate_Set()
    {
        // Arrange
        var box = new Box(new Candy("Snickers", 2), "Yellow");
        var newCandy = new Candy("Mars", 3);
        var candyLens = new Lens<Box, Candy>(b => b.Candy, (c, b) => new Box(b.Candy = c, b.Color));
        
        // Act
        var newBox = candyLens.Set(candyLens, box, newCandy);

        // Assert
        Assert.Equal(newCandy.Name, newBox.Candy.Name);
    }
}

class Box
{
    public string Color;
    public Candy Candy;

    public Box(Candy candy, string color)
    {
        Candy = candy;
        Color = color;
    }
};

class Candy
{
    public string Name;
    private int Price;

    public Candy(string name, int price)
    {
        Name = name;
        Price = price;
    }
};