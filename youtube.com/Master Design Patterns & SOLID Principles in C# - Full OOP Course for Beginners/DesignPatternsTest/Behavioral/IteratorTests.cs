using System;
using System.Collections.Generic;
using System.Linq;
using DesignPatterns.Behavioral.Iterator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DesignPatternsTest.Behavioral;

[TestClass]
public class IteratorTests
{
    [TestMethod]
    public void ShopingList_Push_AddsItem()
    {
        // Arrange
        var expectedItem = "Expected Item";
        var shoppingList = new ShoppingList();

        // Act
        shoppingList.Push(expectedItem);
        var iterator = shoppingList.CreateIterator();

        // Assert
        Assert.AreEqual(expectedItem, iterator.Current());
    }

    [TestMethod]
    public void ShoppingList_Pop_RemovesItem()
    {
        // Arrange
        var expectedItem = "Expected Item";
        var shoppingList = new ShoppingList();

        // Act
        shoppingList.Push(expectedItem);
        var actualItem = shoppingList.Pop();
        var iterator = shoppingList.CreateIterator();

        // Assert
        Assert.AreEqual(expectedItem, actualItem);

        bool listIsEmpty = false;
        try
        {
            iterator.Current();
        }
        catch (ArgumentOutOfRangeException exception)
        {
            listIsEmpty = exception!= null;
        }
        finally
        {

            Assert.IsTrue(listIsEmpty);
        }
    }

    [TestMethod]
    public void ShoppingIterator_IteratesThroughList()
    {
        // Arrange
        var expecedList = new List<string> { "Item 1", "Item 2", "Item 3" };
        var shoppingList = new ShoppingList();

        foreach(var item in expecedList)
        {
            shoppingList.Push(item);
        }

        var actualList = new List<string>();
        var iterator = shoppingList.CreateIterator();

        // Act
        while(iterator.HasNext())
        {
            actualList.Add(iterator.Current());
            iterator.Next();
        }
        
        // Assert
        var isSameList = expecedList.SequenceEqual(actualList);
        Assert.IsTrue(isSameList);
    }
}