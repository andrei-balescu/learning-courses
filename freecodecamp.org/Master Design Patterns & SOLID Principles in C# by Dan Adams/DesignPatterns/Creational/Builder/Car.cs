using DesignPatterns.Creational.Builder.Components;

namespace DesignPatterns.Creational.Builder;

public class Car
{
    public CarType CarType { get; private set; }
    public int Seats { get; private set; }
    public bool IsConvertible { get; private set; }
    public Engine Engine { get; private set; }
    public Dashboard Dashboard { get; private set; }
    public Wheels Wheels { get; private set; }
    public GPSNavigator GPSNavigator { get; private set; }

    // Fields specific to this class
    public double Fuel { get; set; }

    public Car(
        CarType carType,
        int seats,
        bool isConvertible,
        Engine engine,
        Dashboard dashboard,
        Wheels wheels,
        GPSNavigator gpsNavigator
    )
    {
        CarType = carType;
        Seats = seats;
        IsConvertible = isConvertible;
        Engine = engine;
        Dashboard = dashboard;
        Wheels = wheels;
        GPSNavigator = gpsNavigator;
    }
}