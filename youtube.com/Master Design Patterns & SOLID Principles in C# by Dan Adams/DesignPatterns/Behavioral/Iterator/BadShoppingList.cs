namespace DesignPatterns.Behavioral.Iterator;

/// <summary>
/// Bad example: the class cannot be iterated over unless the client has access to the internal Items property which is subject to change (i.e to an array).
/// </summary>
public class BadShoppingList
{
    public List<string> Items { get; private set; }

    public BadShoppingList()
    {
        Items = new List<string>();
    }

    public void Push(string itemName)
    {
        Items.Add(itemName);
    }

    public string Pop()
    {
        var lastItem = Items.Last();
        Items.Remove(lastItem);
        return lastItem;
    }
}