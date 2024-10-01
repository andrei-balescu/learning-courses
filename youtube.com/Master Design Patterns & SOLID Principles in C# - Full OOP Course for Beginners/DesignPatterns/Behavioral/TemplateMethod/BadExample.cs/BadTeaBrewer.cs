namespace DesignPatterns.Behavioral.TemplateMethod.BadExample;

public class BadTeaBrewer
{
    public void MakeBeverage()
    {
        BoilWater();
        PourWaterIntoCup();
        Brew();
        AddCondiments();
    }

    private void BoilWater()
    {
        Console.WriteLine("Boiling wated.");
    }

    private void PourWaterIntoCup()
    {
        Console.WriteLine("Pouring water into cup.");
    }

    private void Brew()
    {
        Console.WriteLine("Brewing tea for 3 minutes.");
    }

    private void AddCondiments()
    {
        bool customerWantsCondiments = CustomerWantsCondiments();
        if (customerWantsCondiments)
        {
            Console.WriteLine("Adding lemon to the tea.");
        }
    }

    private bool CustomerWantsCondiments()
    {
        Console.WriteLine("Would you like lemon with your tea? (y/n)");
        var userInput = Console.ReadLine();
        bool addCondiments = (userInput.ToLower() == "y");

        return addCondiments;
    }
}