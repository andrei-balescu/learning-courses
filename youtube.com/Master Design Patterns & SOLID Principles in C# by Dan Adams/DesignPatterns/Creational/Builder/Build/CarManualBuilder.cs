namespace DesignPatterns.Creational.Builder.Build;

public class CarManualBuilder : Builder
{
    public CarManual GetManual()
    {
        var manual = new CarManual(
            this.CarType,
            this.Seats,
            this.IsConvertible,
            this.Engine,
            this.Dashboard,
            this.Wheels,
            this.GPSNavigator
        );

        return manual;
    }
}