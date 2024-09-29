namespace DesignPatterns.Behavioral.State.DocumentState;

public class ModerationState : DocumentState
{
    public ModerationState(Document document) : base (document)
    {

    }

    public override void Publish()
    {
        if (_document.CurrentUserRole == UserRoles.Admin)
        {
            _document.State = new PublishedState(_document);
        }
    }
}