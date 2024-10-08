namespace DesignPatterns.Behavioral.Command.RemoteCommand;

public class TurnLightOnCommand : ICommand
{
    private Light _light;

    public TurnLightOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.TurnOn();
    }
}