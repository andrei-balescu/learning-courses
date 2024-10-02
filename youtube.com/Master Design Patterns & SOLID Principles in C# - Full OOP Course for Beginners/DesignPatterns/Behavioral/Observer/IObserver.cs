namespace DesignPatterns.Behavioral.Observer;

public interface IObserver<TPayload> 
{
    public void Update(TPayload payload);
}