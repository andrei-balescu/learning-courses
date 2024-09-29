using DesignPatterns.Behavioral.State.DocumentState;

namespace DesignPatterns.Behavioral.State;

/**
 * State Pattern - Context component
 */
public class Document
{
    public IDocumentState State { get; private set; }

    public UserRoles CurrentUserRole { get; private set; }

    public Document(UserRoles currentUserRole)
    {
        CurrentUserRole = currentUserRole;
        State = new DraftState();
    }

    public void Publish()
    {
        State = State.Publish(CurrentUserRole);
    }
}