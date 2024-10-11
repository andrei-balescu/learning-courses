using DesignPatterns.Creational.Builder.Components;

namespace DesignPatterns.Creational.Builder.Build;

public abstract class Builder : IBuilder
{
    public CarType CarType { protected get; set; }
    public int Seats { protected get; set; }
    public bool IsConvertible { protected get; set; }
    public Engine Engine { protected get; set; }
    public Dashboard Dashboard { protected get; set; }
    public Wheels Wheels { protected get; set; }
    public GPSNavigator GPSNavigator { protected get; set; }

    public void Reset()
    {
        CarType = default;
        Seats = default;
        IsConvertible = default;
        Engine = default;
        Dashboard = default;
        Wheels = default;
        GPSNavigator = default;
    }
}