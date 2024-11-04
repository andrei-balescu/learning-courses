using DesignPatterns.Creational.Builder.Components;

namespace DesignPatterns.Creational.Builder.Build;

public class BuildDirector
{
    public void ConstructSportsCar(IBuilder builder)
    {
        builder.CarType = CarType.Sports;
        builder.Seats = 2;
        builder.IsConvertible = true;
        builder.Engine = new Engine();
        builder.Dashboard = new Dashboard(hasRevCounter: true);
        builder.Wheels = new Wheels(diameterInInches: 20);
    }

    public void ConstructSUV(IBuilder builder)
    {
        builder.CarType = CarType.SUV;
        builder.Seats = 5;
        builder.IsConvertible = false;
        builder.Engine = new Engine();
        builder.Dashboard = new Dashboard(hasRevCounter: true);
        builder.Wheels = new Wheels(diameterInInches: 19);
        builder.GPSNavigator = new GPSNavigator();
    }
}