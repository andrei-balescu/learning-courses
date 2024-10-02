namespace DesignPatterns.Behavioral.Observer;

/// <summary>
/// .NET also provides it's own <c>IObserved</c>
/// </summary>
/// <typeparam name="TPayload">The type of message being sent.</typeparam>
public interface IDataSourceObserver<TPayload> 
{
    public void Update(TPayload payload);
}