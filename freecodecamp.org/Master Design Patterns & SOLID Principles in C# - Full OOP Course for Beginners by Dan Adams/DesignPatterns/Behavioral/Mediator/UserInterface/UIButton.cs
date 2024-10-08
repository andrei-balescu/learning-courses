namespace DesignPatterns.Behavioral.Mediator.UserInterface;

public class UIButton : UIControl
{
    private bool _isEnabled;
    public bool IsEnabled
    {
        get { return _isEnabled; }
        set 
        {
            _isEnabled = value;
            NotifyStateChanged();
        }
    }
}