namespace DesignPatterns.Structural.Composite;

public class AmazonItemBox : IAmazonItem
{
    private IEnumerable<IAmazonItem> _items;

    public decimal Price
    {
        get 
        {
            decimal totalPrice = _items.Sum(i => i.Price);
            return totalPrice;
        }
    }

    public AmazonItemBox(IEnumerable<IAmazonItem> items)
    {
        _items = items;
    }


}