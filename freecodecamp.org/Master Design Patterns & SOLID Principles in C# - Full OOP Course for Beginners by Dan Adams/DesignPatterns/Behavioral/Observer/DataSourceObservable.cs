namespace DesignPatterns.Behavioral.Observer;

/// <summary>
/// Observer Pattern - Subject component
/// .NET also provides it's own <c>IObservable</c> interface
/// </summary>
/// <typeparam name="TPayload">The type of message being sent.</typeparam>
public class DataSourceObservable<TPayload>
{
    private List<IDataSourceObserver<TPayload>> _observers;

    public DataSourceObservable()
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