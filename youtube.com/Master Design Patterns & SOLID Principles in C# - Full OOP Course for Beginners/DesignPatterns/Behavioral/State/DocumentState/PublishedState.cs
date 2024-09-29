namespace DesignPatterns.Behavioral.State.DocumentState;

public class PublishedState : DocumentState
{
    public PublishedState(Document document) : base (document)
    {

    }

    public override void Publish()
    {
        // do nothing if already in published state
    }
}