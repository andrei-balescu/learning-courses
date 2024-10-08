namespace DesignPatterns.Behavioral.Mediator.UserInterface;

public class UITextBox : UIControl
{
    private string _text;
    public string Text
    {
        get { return _text; }
        set 
        {
            _text = value;
            NotifyStateChanged();
        }
    }
}