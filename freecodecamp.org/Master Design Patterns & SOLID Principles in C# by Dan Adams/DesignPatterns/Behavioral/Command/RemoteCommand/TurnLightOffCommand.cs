namespace DesignPatterns.Behavioral.Command.RemoteCommand;

public class TurnLightOffCommand : ICommand
{
    private Light _light;

    public TurnLightOffCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.TurnOff();
    }
}