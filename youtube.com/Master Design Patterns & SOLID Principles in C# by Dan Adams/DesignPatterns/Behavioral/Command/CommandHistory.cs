namespace DesignPatterns.Behavioral.Command;

public class CommandHistory
{
    public IList<IUndoableCommand> _commandList;

    public CommandHistory()
    {
        _commandList = new List<IUndoableCommand>();
    }

    public void Execute(IUndoableCommand command)
    {
        command.Execute();
        _commandList.Add(command);
    }

    public void UndoLast()
    {
        var lastCommand = _commandList.Last();
        if (lastCommand != null)
        {
            lastCommand.Unexecute();
            _commandList.Remove(lastCommand);
        }
    }

    public void UndoAll()
    {
        while(_commandList.Count != 0)
        {
            UndoLast();
        }
    }
}