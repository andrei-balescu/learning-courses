namespace DesignPatterns.Behavioral.State.DocumentState;

public interface IDocumentState
{
    IDocumentState Publish(UserRoles userRole);
}