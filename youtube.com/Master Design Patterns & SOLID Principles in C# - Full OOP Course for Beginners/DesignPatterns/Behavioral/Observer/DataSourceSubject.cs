namespace DesignPatterns.Behavioral.Observer;

/**
 * Observer Pattern - Subject component
 */
public class DataSourceSubject<TPayload>
{
    private List<IDataSourceObserver<TPayload>> _observers;

    public DataSourceSubject()
    {
        _observers = new List<IDataSourceObserver<TPayload>>();
    }

    public void AddObserver(IDataSourceObserver<TPayload> observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IDataSourceObserver<TPayload> observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers(TPayload payload)
    {
        foreach (IDataSourceObserver<TPayload> observer in _observers)
        {
            observer.Update(payload);
        }
    }
}