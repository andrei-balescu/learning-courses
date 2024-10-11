namespace DesignPatterns.Behavioral.Iterator;

public interface IShoppingIterator<TItem>
{
    void Next();
    bool HasNext();
    TItem Current();
}