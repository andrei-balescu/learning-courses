namespace DesignPatterns.Behavioral.State.DocumentState;

public class DraftState : IDocumentState
{
    private IDocumentContext _documentContext;

    public DraftState(IDocumentContext documentContext)
    {
        _documentContext = documentContext;
    }

    public void Publish()
    {
        IDocumentState currentState = this;
        if (_documentContext.CurrentUserRole == UserRoles.Editor || _documentContext.CurrentUserRole == UserRoles.Admin)
        {
            currentState = new ModerationState(_documentContext);
        }
        _documentContext.State = currentState;
    }
}