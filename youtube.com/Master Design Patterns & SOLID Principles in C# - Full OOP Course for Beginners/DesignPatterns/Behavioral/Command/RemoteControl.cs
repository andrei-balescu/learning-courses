namespace DesignPatterns.Behavioral.Command;

public class RemoteControl
{
    public IRemoteCommand RemoteCommand { private get; set; }

    public RemoteControl(IRemoteCommand remoteCommand)
    {
        RemoteCommand = remoteCommand;
    }

    public void PressButton()
    {
        RemoteCommand.Execute();
    }
}