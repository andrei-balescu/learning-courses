using System;
using System.Collections.Generic;
using System.Linq;
using DesignPatterns.Behavioral.Observer;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DesignPatternsTest;

[TestClass]
public class ObserverTests
{
    [TestMethod]
    public void DataSource_UpdatesObservers()
    {
        // Arrange
        int expectedValue = 5;

        var observerMock = new Mock<IDataSourceObserver<IEnumerable<int>>>();
        var dataSource = new DataSource();

        // Act
        dataSource.AddObserver(observerMock.Object);
        dataSource.Values = new [] { expectedValue };

        // Assert
        observerMock.Verify(m => m.Update(It.Is<IEnumerable<int>>(l => l.Single() == expectedValue)), Times.Once);
    }

    [TestMethod]
    public void DataSource_RemovesObserver()
    {
        // Arrange
        int expectedValue = 5;

        // note: C# already has an IObserver interface in the System namespace
        var observerMock = new Mock<IDataSourceObserver<IEnumerable<int>>>();
        var dataSource = new DataSource();

        // Act
        dataSource.AddObserver(observerMock.Object);
        dataSource.RemoveObserver(observerMock.Object);
        dataSource.Values = new [] { expectedValue };

        // Assert
        observerMock.Verify(m => m.Update(It.Is<IEnumerable<int>>(l => l.Single() == expectedValue)), Times.Never);
    }

    [TestMethod]
    public void BarChart_Update_LogsValue()
    {
        // Arrange
        int[] values = { 1, 2, 3 };
        int expectedCount = values.Length;

        var loggerMock = new Mock<ILogger>();
        var barChart = new BarChart(loggerMock.Object);

        // Act
        barChart.Update(values);

        // Assert
        loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains(expectedCount.ToString(), StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }

    [TestMethod]
    public void Spreadsheet_Update_LogsValue()
    {
        // Arrange
        int[] values = { 1, 2, 3 };
        int expectedTotal = values.Sum();

        var loggerMock = new Mock<ILogger>();
        var spreadsheet = new Spreadsheet(loggerMock.Object);

        // Act
        spreadsheet.Update(values);

        // Assert
        loggerMock.Verify(m => m.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((o, t) => 
                o.ToString().Contains(expectedTotal.ToString(), StringComparison.InvariantCultureIgnoreCase)),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception?, string>>()
        ), Times.Once);
    }
}