namespace DesignPatterns.Behavioral.State.DocumentState;

public abstract class DocumentState : IDocumentState
{
    protected Document _document;

    public DocumentState(Document document)
    {
        _document = document;
    }

    public abstract void Publish();
}