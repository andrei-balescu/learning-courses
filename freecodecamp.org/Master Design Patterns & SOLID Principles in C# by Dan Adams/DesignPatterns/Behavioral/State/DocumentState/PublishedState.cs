namespace DesignPatterns.Behavioral.State.DocumentState;

public class PublishedState : IDocumentState
{
    private IDocumentContext _documentContext;

    public PublishedState(IDocumentContext documentContext)
    {
        _documentContext = documentContext;
    }

    public void Publish()
    {
        // do nothing if already in published state
    }
}