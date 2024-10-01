namespace DesignPatterns.Behavioral.TemplateMethod.BadExample;

public class BadCoffeeBrewer
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
        Console.WriteLine("Boiling water.");
    }

    private void PourWaterIntoCup()
    {
        Console.WriteLine("Pouring water into cup.");
    }

    private void Brew()
    {
        Console.WriteLine("Brewing coffee for 5 minutes.");
    }

    private void AddCondiments()
    {
        bool customerWantsCondiments = CustomerWantsCondiments();
        if (customerWantsCondiments)
        {
            Console.WriteLine("Adding cream to the coffee.");
        }
    }

    private bool CustomerWantsCondiments()
    {
        Console.WriteLine("Would you like cream with your coffee? (y/n)");
        var userInput = Console.ReadLine();
        bool addCondiments = (userInput.ToLower() == "y");

        return addCondiments;
    }
}