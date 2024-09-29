namespace DesignPatterns.Behavioral.State.BadExample;

/**
 * Bad example: Violates  the Single Responsibility Principle and the Open / Closed Principle
 */
public class BadDocument
{
    public BadDocumentStates State { get; set; }
    public UserRoles CurrentUserRole { get; set; }

    public void Publish()
    {
        if (State == BadDocumentStates.Draft)
        {
            if (CurrentUserRole == UserRoles.Editor || CurrentUserRole == UserRoles.Admin)
            {
                State = BadDocumentStates.Moderation;
            }
        }
        else if (State == BadDocumentStates.Moderation)
        {
            if (CurrentUserRole == UserRoles.Admin)
            {
                State = BadDocumentStates.Published;
            }
        }
        else if (State == BadDocumentStates.Published)
        {
            // do nothing
        }
    }
}