using DesignPatterns.Creational.Builder.Components;

namespace DesignPatterns.Creational.Builder.Build;

public interface IBuilder
{
    public CarType CarType { set; }
    public int Seats { set; }
    public bool IsConvertible { set; }
    public Engine Engine { set; }
    public Dashboard Dashboard { set; }
    public Wheels Wheels { set; }
    public GPSNavigator GPSNavigator { set; }
}