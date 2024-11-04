namespace DesignPatterns.Behavioral.Command.BadExample;

/// <summary>
/// Bad example: RemoteControl is tightly coupled to Light
/// </summary>
public class BadRemoteControl
{
    private BadLight _light;

    public BadRemoteControl(BadLight light)
    {
        _light = light;
    }

    public void PressButton(bool turnOn)
    {
        if (turnOn)
        {
            _light.TurnOn();
        }
        else
        {
            _light.TurnOff();
        }
    }
}