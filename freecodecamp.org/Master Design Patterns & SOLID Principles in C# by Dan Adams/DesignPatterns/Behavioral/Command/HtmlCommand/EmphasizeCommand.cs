namespace DesignPatterns.Behavioral.Command.HtmlCommand;


public class EmphasizeCommand : HtmlUndoableCommand
{
    public EmphasizeCommand(HtmlDocument htmlDocument) : base(htmlDocument)
    {

    }

    public override void Execute()
    {
        base.Execute();
        _htmlDocument.Emphasize();
    }
}