using DesignPatterns.Creational.Builder.Components;

namespace DesignPatterns.Creational.Builder.Build;

public interface IBuilder
{
    CarType CarType { set; }
    int Seats { set; }
    bool IsConvertible { set; }
    Engine Engine { set; }
    Dashboard Dashboard { set; }
    public Wheels Wheels { set; }
    GPSNavigator GPSNavigator { set; }

    void Reset();
}