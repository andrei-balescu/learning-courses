namespace DesignPatterns.Behavioral.Mediator.BadExample;

public class BadButton : BadUIControl
{
    private bool _isEnabled;
    public bool IsEnabled
    {
        get { return _isEnabled; }
        set 
        {
            _isEnabled = value;
            _owner.Changed(this);
        }
    }

    public BadButton(BadDialogBox owner) : base(owner)
    {
    }
}