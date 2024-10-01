namespace DesignPatterns.Behavioral.Command.HtmlCommand;

public abstract class HtmlUndoableCommand : IUndoableCommand
{
    protected HtmlDocument _htmlDocument;

    /**
     * Using Memento Pattern to store snapshot of previous state.
     */
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