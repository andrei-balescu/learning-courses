namespace DesignPatterns.Behavioral.State.DocumentState;

public interface IDocumentContext
{
    public UserRoles CurrentUserRole { get; }

    public IDocumentState State { set; }
}