namespace DesignPatterns.Behavioral.Mediator.BadExample;

public class BadTextBox : BadUIControl
{
    private string _text;
    public string Text
    {
        get { return _text; }
        set 
        {
            _text = value;
            _owner.Changed(this);
        }
    }

    public BadTextBox(BadDialogBox owner) : base(owner)
    {
    }
}