namespace DesignPatterns.Behavioral.Command.HtmlCommand;

public abstract class HtmlUndoableCommand : IUndoableCommand
{
    protected HtmlDocument _htmlDocument;

    /// <summary>
    /// Using Memento Pattern to store snapshot of previous state.
    /// </summary>
    protected string _previousState;

    public HtmlUndoableCommand(HtmlDocument htmlDocument)
    {
        _htmlDocument = htmlDocument;
    }

    public virtual void Execute()
    {
        _previousState = _htmlDocument.Content;
    }

    public void Unexecute()
    {
        _htmlDocument.Content = _previousState;
    }
}