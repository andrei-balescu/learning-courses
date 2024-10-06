namespace DesignPatterns.Structural.Composite;

public class AmazonItem : IAmazonItem
{
    public decimal Price { get; private set; }

    public AmazonItem(decimal price)
    {
        Price = price;
    }
}