namespace DesignPatterns.Behavioral.Mediator.UserInterface;

public abstract class UIControl
{
    public delegate void StateChangedHandler();

    public event StateChangedHandler? StateChanged;

    protected void NotifyStateChanged()
    {
        if (StateChanged != null)
        {
            StateChanged();
        }
    }
}