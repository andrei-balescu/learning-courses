
using DesignPatterns.Behavioral.Interpreter.Expressions;

namespace DesignPatterns.Behavioral.Interpreter;

public class HardCodedInterpreter
{
    private InterpreterContext _context;

    public HardCodedInterpreter(InterpreterContext context)
    {
        _context = context;
    }

    public int Interpret(string input)
    {
        IExpression expressionTree = BuildExpressionTree(input);
        int interpretedExpression = expressionTree.Interpret(_context);
        return interpretedExpression;
    }

    /// <summary>Hardcoded function</summary>
    /// <param name="input">Hardcoded to: 1 + 2 * 3"</param>
    /// <returns>Expression tree based on hardcoded input.</returns>
    private IExpression BuildExpressionTree(string input)
    {
        input = "1 + 2 * 3";

        var expressionTree = new AdditionExpression(
            new MultiplicationExpression(
                new NumberExpression("2"),
                new NumberExpression("3")
            ),
            new NumberExpression("1")
        );

        return expressionTree;
    }
}