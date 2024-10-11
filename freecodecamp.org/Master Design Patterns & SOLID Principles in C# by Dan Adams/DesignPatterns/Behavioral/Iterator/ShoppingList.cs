namespace DesignPatterns.Behavioral.Iterator;

/// <summary>Iterator Pattern - Collection (aggregate) component</summary>
public class ShoppingList
{
    public IList<string> Items { get; private set; }

    public ShoppingList()
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

    public IShoppingIterator<string> CreateIterator()
    {
        var iterator = new ShoppingIterator(this);
        return iterator;
    }

    private class ShoppingIterator : IShoppingIterator<string>
    {
        private ShoppingList _shoppingList;

        private int _index;

        public ShoppingIterator(ShoppingList shoppingList)
        {
            _shoppingList = shoppingList;
        }

        public string Current()
        {
            var currentItem = _shoppingList.Items[_index]; 
            return currentItem;
        }

        public bool HasNext()
        {
            var hasNext = _index < _shoppingList.Items.Count;
            return hasNext;
        }

        public void Next()
        {
            _index++;
        }
    }
}