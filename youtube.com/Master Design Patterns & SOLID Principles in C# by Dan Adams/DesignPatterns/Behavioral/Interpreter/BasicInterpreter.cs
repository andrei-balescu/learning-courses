
using DesignPatterns.Behavioral.Interpreter.Expressions;

namespace DesignPatterns.Behavioral.Interpreter;

/// <summary>Only handles additions and subtractions.</summary>
public class BasicInterpreter
{
    private InterpreterContext _context;

    public BasicInterpreter(InterpreterContext context)
    {
        _context = context;
    }

    public int Interpret(string input)
    {
        IExpression expressionTree = BuildExpressionTree(input);
        int interpretedExpression = expressionTree.Interpret(_context);
        return interpretedExpression;
    }

    /// <summary>Only handles additions and subtractions.</summary>
    /// <param name="input">Mathemethical expression separated by single spaces.</param>
    /// <returns>Expression tree for mathematical expresion.</returns>
    private IExpression BuildExpressionTree(string input)
    {
        Queue<string> inputQueue = ParseInputString(input);

        var expressionStack = new Stack<IExpression>();

        while(inputQueue.Count > 0)
        {
            string token = inputQueue.Dequeue();

            if (int.TryParse(token,  out int number))
            {
                expressionStack.Push(new NumberExpression(number));
            }
            else if (token == "+" || token =="-")
            {
                var rightExpression = expressionStack.Pop();
                var leftExpression = expressionStack.Pop();

                if (token == "+")
                {
                    expressionStack.Push(new AdditionExpression(leftExpression, rightExpression));
                }
                else if (token == "-")
                {
                    expressionStack.Push(new SubtractionExpression(leftExpression, rightExpression));
                }
            }
        }

        IExpression finalExpression = expressionStack.Pop();
        return finalExpression;
    }



    /// <summary>Using Shunting Yard algorithm to convert infix (A+B) to postfix (AB+).</summary>
    /// <param name="input">The input string to parse</param>
    /// <returns>A queue of numbers and operations.</returns>
    private Queue<string> ParseInputString(string input)
    {
        string[] tokens = input.Split(" ");
        var output = new Queue<string>();
        var operators = new Stack<string>();

        string numberOperator;
        foreach (string token in tokens)
        {
            if (int.TryParse(token, out _))
            {
                output.Enqueue(token);
            }
            else if (token == "+" || token == "-")
            {
                while(operators.Count > 0 && (operators.Peek() == "+" || operators.Peek() == "-"))
                {
                    numberOperator = operators.Pop();
                    output.Enqueue(numberOperator);
                }
                operators.Push(token);
            }
        }
        numberOperator = operators.Pop();
        output.Enqueue(numberOperator);

        return output;
    }
}