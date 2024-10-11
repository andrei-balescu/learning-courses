namespace DesignPatterns.Behavioral.Command;

public class RemoteControl
{
    public ICommand RemoteCommand { private get; set; }

    public RemoteControl(ICommand remoteCommand)
    {
        RemoteCommand = remoteCommand;
    }

    public void PressButton()
    {
        RemoteCommand.Execute();
    }
}