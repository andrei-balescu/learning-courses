using DesignPatterns.Creational.Builder.Components;

namespace DesignPatterns.Creational.Builder;

public class CarManual
{
    public CarType CarType { get; private set; }
    public int Seats { get; private set; }
    public bool IsConvertible { get; private set; }
    public Engine Engine { get; private set; }
    public Dashboard Dashboard { get; private set; }
    public Wheels Wheels { get; private set; }
    public GPSNavigator GPSNavigator { get; private set; }

    public CarManual(
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

    public string Print()
    {
        var text = string.Empty;
        text += $"Car type: {CarType}\n";
        if (IsConvertible)
        {
            text += "Convertible\n";
        }
        text += $"Seats: {Seats}\n";
        text += $"Wheels: diameter in inches = {Wheels.DiameterInInches}\n";
        text += $"Engine: info on engine... \n";
        text += $"GPS Navigator: ";
        if (GPSNavigator != null)
        {
            text += "Info on GPS\n";
        }
        else
        {
            text += "N/A\n";
        }

        return text;
    }
}