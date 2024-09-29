namespace DesignPatterns.Behavioral.State.DocumentState;

public class ModerationState : IDocumentState
{
    public IDocumentState Publish(UserRoles userRole)
    {
        IDocumentState currentState = this;
        if (userRole == UserRoles.Admin)
        {
            currentState = new PublishedState();
        }
        return currentState;
    }
}