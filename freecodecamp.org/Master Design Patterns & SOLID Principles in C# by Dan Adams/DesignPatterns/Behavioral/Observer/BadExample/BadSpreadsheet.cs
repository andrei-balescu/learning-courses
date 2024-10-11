namespace DesignPatterns.Behavioral.Observer.BadExample;

public class Spreadsheet
{
    public void CalculateTotal(IEnumerable<int> values)
    {
        int total = values.Sum();
        Console.WriteLine($"Values total is {total}");
    }
}