namespace DesignPatterns.Behavioral.State.DocumentState;

public class DraftState : DocumentState
{
    public DraftState(Document document) : base(document)
    {
        
    }

    public override void Publish()
    {
        if (_document.CurrentUserRole == UserRoles.Editor || _document.CurrentUserRole == UserRoles.Admin)
        {
            _document.State = new ModerationState(_document);
        }
    }
}