namespace DesignPatterns.Behavioral.Observer.BadExample;

public class BadBarchart
{
    public void Render(IEnumerable<int> values)
    {
        Console.WriteLine($"Rendering bar chart with {values.Count()} values");
    }
}