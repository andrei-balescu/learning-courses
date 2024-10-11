namespace DesignPatterns.Behavioral.Mediator.BadExample;

public class BadListBox : BadUIControl
{
    private string _selection;

    public string Selection
    {
        get { return _selection; }
        set 
        {
            _selection = value;
            _owner.Changed(this);
        }
    }

    public BadListBox(BadDialogBox owner) : base(owner)
    {
    }
}