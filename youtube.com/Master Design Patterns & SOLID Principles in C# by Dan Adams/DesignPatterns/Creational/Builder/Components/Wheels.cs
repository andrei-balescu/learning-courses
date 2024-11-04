namespace DesignPatterns.Creational.Builder.Components;

public class Wheels
{
    public float DiameterInInches { get; private set; }

    public Wheels(float diameterInInches)
    {
        DiameterInInches = diameterInInches;
    }
}