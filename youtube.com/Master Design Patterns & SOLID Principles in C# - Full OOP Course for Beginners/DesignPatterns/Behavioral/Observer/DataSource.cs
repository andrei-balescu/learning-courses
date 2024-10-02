namespace DesignPatterns.Behavioral.Observer;

public class DataSource : Subject<IEnumerable<int>>
{
    private IEnumerable<int> _values;
    public IEnumerable<int> Values
    {
        get => _values;
        set
        {
            _values = value;
            NotifyObservers(_values);
        }
    }
}