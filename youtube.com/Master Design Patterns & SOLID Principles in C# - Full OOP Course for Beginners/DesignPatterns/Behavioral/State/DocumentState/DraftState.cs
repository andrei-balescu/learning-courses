namespace DesignPatterns.Behavioral.State.DocumentState;

public class DraftState : IDocumentState
{
    public IDocumentState Publish(UserRoles userRole)
    {
        IDocumentState currentState = this;
        if (userRole == UserRoles.Editor || userRole == UserRoles.Admin)
        {
            currentState = new ModerationState();
        }
        return currentState;
    }
}