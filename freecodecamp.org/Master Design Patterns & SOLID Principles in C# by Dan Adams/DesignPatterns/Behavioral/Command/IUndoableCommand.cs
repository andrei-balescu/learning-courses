namespace DesignPatterns.Behavioral.Command;

public interface IUndoableCommand : ICommand
{
    void Unexecute();
}