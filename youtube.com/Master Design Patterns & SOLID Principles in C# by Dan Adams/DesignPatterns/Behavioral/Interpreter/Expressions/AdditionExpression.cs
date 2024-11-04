namespace DesignPatterns.Behavioral.Interpreter.Expressions;

public class AdditionExpression : IExpression
{
    private IExpression _left;
    private IExpression _right;

    public AdditionExpression(IExpression left, IExpression right)
    {
        _left = left;
        _right = right;
    }

    public int Interpret(InterpreterContext context)
    {
        int interpretedLeft = _left.Interpret(context);
        int interpretedRight = _right.Interpret(context);

        int result = interpretedLeft + interpretedRight;
        return result;
    }
}