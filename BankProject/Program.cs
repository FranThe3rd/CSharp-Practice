using System;
using System.Collections.Generic;

// Base BankAccount class
public class BankAccount
{
    public int AccountNumber { get; set; }
    public string AccountHolder { get; set; }
    public int Balance { get; set; }

    public BankAccount(int accountNumber, string accountHolder, int balance)
    {
        AccountNumber = accountNumber;
        AccountHolder = accountHolder;
        Balance = balance;
    }

    public void Deposit(int amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Must Deposit A Positive Amount");
            return;
        }
        Balance += amount;
        Console.WriteLine($"Deposited ${amount}. New Balance: ${Balance}");
    }

    public virtual void Withdraw(int amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Must Withdraw A Positive Amount");
        }
        else if (amount > Balance)
        {
            Console.WriteLine("Cannot Withdraw Money: Insufficient Balance");
        }
        else
        {
            Balance -= amount;
            Console.WriteLine($"Withdrew ${amount}. New Balance: ${Balance}");
        }
    }

    public void DisplayBankInfo()
    {
        Console.WriteLine($"Account Number: {AccountNumber}\nAccount Holder: {AccountHolder}\nBalance: ${Balance}\n");
    }
}

// SavingsAccount class
public class SavingsAccount : BankAccount
{
    public int InterestRate { get; set; }

    public SavingsAccount(int accountNumber, string accountHolder, int balance, int interestRate)
        : base(accountNumber, accountHolder, balance)
    {
        InterestRate = interestRate;
    }

    public void ApplyInterest()
    {
        int interest = Balance * InterestRate / 100;
        Balance += interest;
        Console.WriteLine($"Interest of ${interest} applied. New Balance: ${Balance}");
    }

    public override void Withdraw(int amount)
    {
        if (amount > Balance)
        {
            Console.WriteLine("Cannot Withdraw Money: Savings Account cannot be overdrawn.");
        }
        else
        {
            base.Withdraw(amount);
        }
    }
}

// CurrentAccount class
public class CurrentAccount : BankAccount
{
    public int OverdraftLimit { get; set; }

    public CurrentAccount(int accountNumber, string accountHolder, int balance, int overDraftLimit)
        : base(accountNumber, accountHolder, balance)
    {
        OverdraftLimit = overDraftLimit;
    }

    public override void Withdraw(int amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Must Withdraw A Positive Amount");
        }
        else if ((Balance - amount) < -OverdraftLimit)
        {
            Console.WriteLine($"Cannot Withdraw That Much: Overdraft Limit ${OverdraftLimit} Exceeded");
        }
        else
        {
            Balance -= amount;
            Console.WriteLine($"Withdrew ${amount}. New Balance: ${Balance}");
        }
    }
}

// Bank class
public class Bank
{
    private List<BankAccount> accounts = new List<BankAccount>();

    public void AddAccount(BankAccount account)
    {
        accounts.Add(account);
        Console.WriteLine("Account added successfully!");
    }

    public BankAccount GetAccount(int accountNumber)
    {
        return accounts.Find(a => a.AccountNumber == accountNumber);
    }

    public void Transfer(int fromAccountNumber, int toAccountNumber, int amount)
    {
        BankAccount from = GetAccount(fromAccountNumber);
        BankAccount to = GetAccount(toAccountNumber);

        if (from == null || to == null)
        {
            Console.WriteLine("Invalid account number(s).");
            return;
        }

        from.Withdraw(amount);
        to.Deposit(amount);
        Console.WriteLine($"Transferred ${amount} from account {fromAccountNumber} to {toAccountNumber}");
    }

    public void DisplayAllAccounts()
    {
        foreach (var account in accounts)
        {
            account.DisplayBankInfo();
        }
    }
}

// Program with console menu
public class Program
{
    public static void Main()
    {
        Bank bank = new Bank();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n--- Bank Menu ---");
            Console.WriteLine("1. Create Account (Savings or Current)");
            Console.WriteLine("2. Deposit");
            Console.WriteLine("3. Withdraw");
            Console.WriteLine("4. Transfer");
            Console.WriteLine("5. Apply Interest (Savings Only)");
            Console.WriteLine("6. Show Account Details");
            Console.WriteLine("7. Show All Accounts");
            Console.WriteLine("8. Exit");
            Console.Write("Choose an option: ");

            string input = Console.ReadLine();
            Console.WriteLine();

            switch (input)
            {
                case "1":
                    Console.Write("Enter Account Number: ");
                    int accNum = int.Parse(Console.ReadLine());
                    Console.Write("Enter Account Holder Name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter Initial Balance: ");
                    int bal = int.Parse(Console.ReadLine());
                    Console.Write("Type of Account (Savings/Current): ");
                    string type = Console.ReadLine().ToLower();

                    if (type == "savings")
                    {
                        Console.Write("Enter Interest Rate (%): ");
                        int rate = int.Parse(Console.ReadLine());
                        bank.AddAccount(new SavingsAccount(accNum, name, bal, rate));
                    }
                    else if (type == "current")
                    {
                        Console.Write("Enter Overdraft Limit: ");
                        int limit = int.Parse(Console.ReadLine());
                        bank.AddAccount(new CurrentAccount(accNum, name, bal, limit));
                    }
                    else
                    {
                        Console.WriteLine("Invalid account type.");
                    }
                    break;

                case "2":
                    Console.Write("Enter Account Number: ");
                    int depAcc = int.Parse(Console.ReadLine());
                    Console.Write("Enter Deposit Amount: ");
                    int depAmt = int.Parse(Console.ReadLine());
                    var depAccount = bank.GetAccount(depAcc);
                    if (depAccount != null) depAccount.Deposit(depAmt);
                    else Console.WriteLine("Account not found.");
                    break;

                case "3":
                    Console.Write("Enter Account Number: ");
                    int witAcc = int.Parse(Console.ReadLine());
                    Console.Write("Enter Withdraw Amount: ");
                    int witAmt = int.Parse(Console.ReadLine());
                    var witAccount = bank.GetAccount(witAcc);
                    if (witAccount != null) witAccount.Withdraw(witAmt);
                    else Console.WriteLine("Account not found.");
                    break;

                case "4":
                    Console.Write("Enter From Account Number: ");
                    int fromAcc = int.Parse(Console.ReadLine());
                    Console.Write("Enter To Account Number: ");
                    int toAcc = int.Parse(Console.ReadLine());
                    Console.Write("Enter Amount to Transfer: ");
                    int trAmt = int.Parse(Console.ReadLine());
                    bank.Transfer(fromAcc, toAcc, trAmt);
                    break;

                case "5":
                    Console.Write("Enter Account Number: ");
                    int intAcc = int.Parse(Console.ReadLine());
                    var intAccount = bank.GetAccount(intAcc) as SavingsAccount;
                    if (intAccount != null) intAccount.ApplyInterest();
                    else Console.WriteLine("Savings account not found.");
                    break;

                case "6":
                    Console.Write("Enter Account Number: ");
                    int showAcc = int.Parse(Console.ReadLine());
                    var showAccount = bank.GetAccount(showAcc);
                    if (showAccount != null) showAccount.DisplayBankInfo();
                    else Console.WriteLine("Account not found.");
                    break;

                case "7":
                    bank.DisplayAllAccounts();
                    break;

                case "8":
                    running = false;
                    Console.WriteLine("Exiting program...");
                    break;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }
}
