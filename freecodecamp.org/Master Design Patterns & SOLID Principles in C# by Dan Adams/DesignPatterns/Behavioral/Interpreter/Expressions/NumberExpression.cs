namespace DesignPatterns.Behavioral.Interpreter.Expressions;

public class NumberExpression : IExpression
{
    private int _number;

    public NumberExpression(int number)
    {
        _number = number;
    }

    public NumberExpression(string number)
    {
        _number = int.Parse(number);
    }

    public int Interpret(InterpreterContext context)
    {
        return _number;
    }
}