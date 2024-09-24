using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OopPrinciples.Encapsulation;

namespace OopPrinciplesTest;

[TestClass]
public class EncapsulationTests
{
    private BankAccount? _bankAccount;


    [TestMethod]
    public void ConstructorSetsBalance()
    {
        decimal expectedBalance = 100;
        _bankAccount = new BankAccount(expectedBalance);
        decimal actualBalance = _bankAccount.Balance;

        Assert.AreEqual(expectedBalance, actualBalance);
    }

    [TestMethod]
    public void DepositThrowsErrorOnNegativeBalance()
    {
        bool isException = false;
        try
        {
            _bankAccount = new BankAccount(0);
        }
        catch (ArgumentException exception)
        {
            isException = (exception != null);
        }
        finally
        {
            Assert.IsTrue(isException);
        }
    }

    [TestMethod]
    public void DepositAddsValue()
    {
        decimal initialBalance = 100;
        decimal addedBalance = 50;
        decimal expectedBalance = initialBalance + addedBalance;

        _bankAccount = new BankAccount(initialBalance);
        _bankAccount.Deposit(addedBalance);
        decimal actualBalance = _bankAccount.Balance;

        Assert.AreEqual(expectedBalance, actualBalance);
    }

    [TestMethod]
    public void WithdrawThrowsErrorOnNegativeBalance()
    {
        decimal initialBalance = 10;
        decimal withdrawBalance = 0;

        bool isException = false;
        try
        {
            _bankAccount = new BankAccount(initialBalance);
            _bankAccount.Withdraw(withdrawBalance);
        }
        catch (ArgumentException exception)
        {
            isException = (exception != null);
        }
        finally
        {
            Assert.IsTrue(isException);
        }
    }

    [TestMethod]
    public void WithdrawThrowsErrorOnInsufficientFunds()
    {
        decimal initialBalance = 10;
        decimal withdrawBalance = 100;

        bool isException = false;
        try
        {
            _bankAccount = new BankAccount(initialBalance);
            _bankAccount.Withdraw(withdrawBalance);
        }
        catch (InvalidOperationException exception)
        {
            isException = (exception != null);
        }
        finally
        {
            Assert.IsTrue(isException);
        }
    }

    [TestMethod]
    public void WithdrawSubtractsValue()
    {
        decimal initialBalance = 100;
        decimal withdrawalBalance = 50;
        decimal expectedBalance = initialBalance - withdrawalBalance;

        _bankAccount = new BankAccount(initialBalance);
        _bankAccount.Withdraw(withdrawalBalance);
        decimal actualBalance = _bankAccount.Balance;

        Assert.AreEqual(expectedBalance, actualBalance);
    }
}