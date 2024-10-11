namespace DesignPatterns.Behavioral.State.DocumentState;

public class ModerationState : IDocumentState
{
    private IDocumentContext _documentContext;

    public ModerationState(IDocumentContext documentContext)
    {
        _documentContext = documentContext;
    }

    public void Publish()
    {
        IDocumentState currentState = this;
        if (_documentContext.CurrentUserRole == UserRoles.Admin)
        {
            currentState = new PublishedState(_documentContext);
        }
        _documentContext.State = currentState;
    }
}