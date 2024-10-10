namespace DesignPatterns.Creational.Builder.Build;

public class CarBuilder : Builder
{
    public Car GetCar()
    {
        var car = new Car(
            this.CarType,
            this.Seats,
            this.IsConvertible,
            this.Engine,
            this.Dashboard,
            this.Wheels,
            this.GPSNavigator
        );
        return car;
    }
}