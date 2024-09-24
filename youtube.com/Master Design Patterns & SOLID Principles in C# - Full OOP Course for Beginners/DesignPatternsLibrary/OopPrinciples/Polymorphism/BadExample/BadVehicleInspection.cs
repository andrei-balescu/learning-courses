namespace DesignPatternsLibrary.OopPrinciples.Polymorphism.BadExample;

public class BadVehicleInspection
{
    public void InspectVehicles(IEnumerable<object> vehicles){
        foreach (var vehicle in vehicles)
        {
            if (vehicle is BadCar)
            {
                var car = (BadCar)vehicle;
                car.Start();
                car.Stop();
            }
            else if (vehicle is BadMotorcycle)
            {
                var motorcycle = (BadMotorcycle)vehicle;
                motorcycle.Start();
                motorcycle.Stop();
            }
            // else if...
        }
    }
}
