using DesignPatterns.Behavioral.Interpreter;
using DesignPatterns.Behavioral.Interpreter.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignPatternsTest.Behavioral;

[TestClass]
public class InterpreterTests
{
    private InterpreterContext _emptyContext = new InterpreterContext();

    [TestMethod]
    public void NumberExpression_Interpret_ParsesNumber()
    {
        // Arrange
        int expectedNumber = 50;
        var numberExpression = new NumberExpression(expectedNumber.ToString());

        // Act
        int actualNumber = numberExpression.Interpret(_emptyContext);

        // Assert
        Assert.AreEqual(expectedNumber, actualNumber);
    }

    [TestMethod]
    public void AdditionExpression_Interpret_CalculatesSum()
    {
        // Arrange
        int leftNumber = 2;
        int rightNumber = 3;
        int expectedSum = leftNumber + rightNumber;

        var leftExpression = new NumberExpression(leftNumber);
        var rightExpression = new NumberExpression(rightNumber);
        var additionExpression = new AdditionExpression(leftExpression, rightExpression);

        // Act
        var actualSum = additionExpression.Interpret(_emptyContext);

        // Assert
        Assert.AreEqual(expectedSum, actualSum);
    }

    [TestMethod]
    public void SubtractionExpression_Interpret_CalculatesSubtraction()
    {
        int leftNumber = 2;
        int rightNumber = 3;
        int expectedSubtraction = leftNumber - rightNumber;

        var leftExpression = new NumberExpression(leftNumber);
        var rightExpression = new NumberExpression(rightNumber);
        var subtractionExpression = new SubtractionExpression(leftExpression, rightExpression);

        // Act
        var actualSubtraction = subtractionExpression.Interpret(_emptyContext);

        // Assert
        Assert.AreEqual(expectedSubtraction, actualSubtraction);
    }

    [TestMethod]
    public void MultiplicationExpression_Interpret_CalculatesMultiplication()
    {
        int leftNumber = 2;
        int rightNumber = 3;
        int expectedMultiplication = leftNumber * rightNumber;

        var leftExpression = new NumberExpression(leftNumber);
        var rightExpression = new NumberExpression(rightNumber);
        var multiplicationExpression = new MultiplicationExpression(leftExpression, rightExpression);

        // Act
        var actualMultiplication = multiplicationExpression.Interpret(_emptyContext);

        // Assert
        Assert.AreEqual(expectedMultiplication, actualMultiplication);
    }

    [TestMethod]
    public void HardcodedInterpreter_Interpret_CalculatesHardcodedExpression()
    {
        // Arrange
        int expectedCalculation = 1 + 2 * 3;
        var interpreter = new HardCodedInterpreter(_emptyContext);

        // Act
        int actualCalculation = interpreter.Interpret(string.Empty);

        // Assert
        Assert.AreEqual(expectedCalculation, actualCalculation);
    }

    [TestMethod]
    public void BasicInterpreter_Interpret_CalculatesHardcodedExpression()
    {
        // Arrange
        string input = "1 + 5 - 4 - 7 - 2 + 11";
        int expectedCalculation = 1 + 5 - 4 - 7 - 2 + 11;
        var interpreter = new BasicInterpreter(_emptyContext);

        // Act
        int actualCalculation = interpreter.Interpret(input);

        // Assert
        Assert.AreEqual(expectedCalculation, actualCalculation);
    }
}