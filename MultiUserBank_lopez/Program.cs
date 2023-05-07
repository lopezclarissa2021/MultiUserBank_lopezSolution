using MultiUserBank_lopez.Models;
/// <summary>
/// Clarissa Lopez
/// IT112
/// </summary>
namespace MultiUserBank_lopez
{
    internal class Program
    {
        static Bank bank = new Bank();
        private static bool Exit = false;
        static void Main(string[] args)
        {
            while (!Exit)
            {
                if (!bank.IsAuthenticated)
                {
                    Console.WriteLine($"Bank Balance: {bank.BankBalance.ToString("C")}");
                    Console.WriteLine("Enter Username:");
                    string username = Console.ReadLine();
                    Console.WriteLine("Enter Password:");
                    string password = Console.ReadLine();
                    User user = bank.AuthenticateUser(username, password);
                    if (user == null)
                        Console.WriteLine("User not authorized");
                    Console.WriteLine($"{user.Username}'s balance: {user.Balance.ToString("C")}" + Environment.NewLine);
                }

                Console.WriteLine(bank.AvailableTransactions);
                string transaction = Console.ReadLine();

                if (transaction == TransactionType.Deposit)
                {
                    Console.WriteLine(bank.DepositText);
                    string depositAmount = Console.ReadLine();
                    decimal amount = decimal.Parse(depositAmount);
                    bank.Deposit(amount);
                    Console.WriteLine($"{bank.CurrentUser.Username}'s balance: {bank.CurrentUser.Balance.ToString("C")}" + Environment.NewLine);
                }

                if (transaction == TransactionType.Withdrawal)
                {
                    Console.WriteLine(bank.WithdrawalText);
                    string withdrawlAmount = Console.ReadLine();
                    decimal amount = decimal.Parse(withdrawlAmount);
                    bank.Withdrawl(amount);
                    Console.WriteLine($"{bank.CurrentUser.Username}'s balance: {bank.CurrentUser.Balance.ToString("C")}" + Environment.NewLine);
                }

                if (transaction == TransactionType.CheckBalance)
                {
                    Console.Write($"{bank.CurrentUser.Username}'s balance: {bank.CurrentUser.Balance.ToString("C")}" + Environment.NewLine);
                }

                if (transaction == TransactionType.Exit)
                {
                    Exit = true;
                }

                if (transaction == TransactionType.Logout)
                {
                    bank.IsAuthenticated = false;
                }
            }
        }
    }
}