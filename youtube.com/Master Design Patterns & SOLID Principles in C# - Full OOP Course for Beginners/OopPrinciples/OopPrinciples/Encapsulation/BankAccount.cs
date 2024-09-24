namespace OopPrinciples.Encapsulation;

public class BankAccount
{
    public decimal Balance { get; private set; }

    public BankAccount(decimal balance)
    {
        Deposit(balance);
    }

    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Deposit amounts must be positive");
        }

        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Withdraw amounts must be positive");
        }

        if (amount > Balance)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        Balance -= amount;

    }
}