using DesignPatterns.Behavioral.State.DocumentState;

namespace DesignPatterns.Behavioral.State;

/// <summary>
/// State Pattern - Context component
/// </summary>
public class Document : IDocumentContext
{
    public IDocumentState State { get; set; }

    public UserRoles CurrentUserRole { get; private set; }

    public Document(UserRoles currentUserRole)
    {
        CurrentUserRole = currentUserRole;
        State = new DraftState(this);
    }

    public void Publish()
    {
        State.Publish();
    }
}