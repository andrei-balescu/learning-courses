namespace DesignPatterns.Behavioral.State.DocumentState;

public class PublishedState : IDocumentState
{
    public IDocumentState Publish(UserRoles userRole)
    {
        // do nothing if already in published state
        return this;
    }
}