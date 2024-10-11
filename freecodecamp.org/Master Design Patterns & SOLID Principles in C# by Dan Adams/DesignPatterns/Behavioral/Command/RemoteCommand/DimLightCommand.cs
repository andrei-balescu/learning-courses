namespace DesignPatterns.Behavioral.Command.RemoteCommand;

public class DimLightCommand : ICommand
{
    private Light _light;

    public DimLightCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.Dim();
    }
}