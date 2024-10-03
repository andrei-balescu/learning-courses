namespace DesignPatterns.Behavioral.Mediator.UserInterface;

public class UIListBox : UIControl
{
    private string _selection;
    public string Selection 
    {
        get { return _selection; } 
        set
        {
            _selection = value;
            NotifyStateChanged();
        }
    }
}