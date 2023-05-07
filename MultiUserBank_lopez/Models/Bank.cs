using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiUserBank_lopez.Models
{
    public class Bank
    {
        public decimal BankBalance { get => Balance; }
        decimal Balance;
        List<User> Users;
        public User CurrentUser;
        public bool IsAuthenticated { get; set; }
        private decimal MaxWithdrawl { get; set; } = 500;
        private decimal MinBalance { get; set; } = 0;
        public readonly string DepositText = "Amount you would like to deposit: ";
        public readonly string WithdrawalText = "Amount you would like to withdrawl: ";
        public Bank()
        {
            Balance = 10000;

            Users = new List<User>()
            {
                new User { Username = "jlennon", Password = "johnny", Balance = 1250 },
                new User { Username = "pmccartney", Password = "pauly", Balance = 2500 },
                new User { Username = "gharrison", Password = "georgy", Balance = 3000 },
                new User { Username = "rstarr", Password = "ringoy", Balance = 1000 }
            };
        }

        public User AuthenticateUser(string username, string password)
        {
            User user = Users.Where(u=> u.Username == username && u.Password == password).FirstOrDefault();
            if (user != null)
            {
                CurrentUser = user;
                IsAuthenticated = true;
            }
            return user;
        }

        public decimal Withdrawl(decimal amount)
        {
            if (amount > MaxWithdrawl)
            {
                amount = MaxWithdrawl;
            }

            CurrentUser.Balance -= amount;
            Balance -= amount;

            if (Balance < MinBalance)
            {
                Balance = MinBalance;
            }

            if (CurrentUser.Balance < MinBalance)
            {
                CurrentUser.Balance = MinBalance;
            }

            return CurrentUser.Balance;
        }

        public string AvailableTransactions
        {
            get
            {
                string availableTransactions = $"{TransactionType.Deposit}: make deposit" + Environment.NewLine;

                if (CurrentUser.Balance > 0 && Balance > 0)
                    availableTransactions += $"{TransactionType.Withdrawal}: withdrawl" + Environment.NewLine;

                availableTransactions += $"{TransactionType.CheckBalance}: Check Balance" + Environment.NewLine;
                availableTransactions += $"{TransactionType.Logout}: Logout" + Environment.NewLine;
                availableTransactions += $"{TransactionType.Exit}: Exit" + Environment.NewLine;
                return availableTransactions;
            }
        }

        public string BalanceText
        {
            get
            {
                return "Your new balance is: " + CurrentUser.Balance.ToString("C");
            }
        }

        public decimal Deposit(decimal amount)
        {
            Balance += amount;
            CurrentUser.Balance += amount;
            return CurrentUser.Balance;
        }
    }
}
