namespace DesignPatternsLibrary.OopPrinciples.Polymorphism;

public class VehicleInspection
{
    public void InspectVehicles(IEnumerable<Vehicle> vehicles)
    {
        foreach(var vehicle in vehicles)
        {
            vehicle.Start();
            vehicle.Stop();
        }
    }
}