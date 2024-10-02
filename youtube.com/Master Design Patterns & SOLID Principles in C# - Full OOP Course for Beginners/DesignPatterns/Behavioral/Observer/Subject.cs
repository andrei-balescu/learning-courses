namespace DesignPatterns.Behavioral.Observer;

/**
 * Observer Pattern - Subject component
 */
public class Subject<TPayload>
{
    private List<IObserver<TPayload>> _observers;

    public Subject()
    {
        _observers = new List<IObserver<TPayload>>();
    }

    public void AddObserver(IObserver<TPayload> observer)
    {
        _observers.Add(observer);
    }

    public void RemoveObserver(IObserver<TPayload> observer)
    {
        _observers.Remove(observer);
    }

    public void NotifyObservers(TPayload payload)
    {
        foreach (IObserver<TPayload> observer in _observers)
        {
            observer.Update(payload);
        }
    }
}