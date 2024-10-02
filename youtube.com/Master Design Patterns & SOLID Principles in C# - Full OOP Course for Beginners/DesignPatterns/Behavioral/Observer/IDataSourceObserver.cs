namespace DesignPatterns.Behavioral.Observer;

public interface IDataSourceObserver<TPayload> 
{
    public void Update(TPayload payload);
}