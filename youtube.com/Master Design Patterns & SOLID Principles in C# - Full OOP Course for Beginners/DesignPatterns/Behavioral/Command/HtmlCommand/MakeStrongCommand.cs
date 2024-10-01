namespace DesignPatterns.Behavioral.Command.HtmlCommand;

public class MakeStrongCommand : HtmlUndoableCommand
{
    public MakeStrongCommand(HtmlDocument htmlDocument) : base(htmlDocument)
    {
    }

    public override void Execute()
    {
        base.Execute();
        _htmlDocument.MakeStrong();
    }
}