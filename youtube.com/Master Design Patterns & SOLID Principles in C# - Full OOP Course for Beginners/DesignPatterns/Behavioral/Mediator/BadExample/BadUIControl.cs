namespace DesignPatterns.Behavioral.Mediator.BadExample;

public class BadUIControl
{
    protected BadDialogBox _owner;

    public BadUIControl(BadDialogBox owner)
    {
        _owner = owner;
    }
}