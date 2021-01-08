#region Usings

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Console = System.Console;

#endregion

namespace DesignPatterns.Behavioral
{
    /*
     ## 1
     The command design pattern is a good choice when you want to implement callbacks, 
        queuing tasks, tracking history, and undo/redo functionality in your application. 
        The command pattern is a good choice for implementing 
        retry mechanisms - when your application would like to reattempt connecting to a service 
        at a later point in time which is not up and running at this moment.  
        The Command pattern is also used in message queuing applications, 
        i.e., in applications that need to recover from data loss.
     
     
     */

    public class BankAccount
    {
        private int balance;
        private readonly int overdraftLimit = -500;

        public void Deposit(int amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited ${amount}, balance is now {balance}");
        }

        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }

        public bool Withdraw(int amount)
        {
            if (balance - amount >= overdraftLimit)
            {
                balance -= amount;
                Console.WriteLine($"Withdrew ${amount}, balance is now {balance}");
                return true;
            }

            return false;
        }
    }

    public interface ICommand
    {
        void Call();
        void Undo();
    }

    public class BankAccountCommand : ICommand
    {
        public enum Action
        {
            Deposit,
            Withdraw
        }

        private readonly BankAccount account;

        private readonly Action action;
        private readonly int amount;
        private bool succeeded;

        public BankAccountCommand(BankAccount account, Action action, int amount)
        {
            this.account = account ?? throw new ArgumentNullException(nameof(account));
            this.action = action;
            this.amount = amount;
        }

        public void Call()
        {
            switch (action)
            {
                case Action.Deposit:
                    account.Deposit(amount);
                    succeeded = true;
                    break;
                case Action.Withdraw:
                    succeeded = account.Withdraw(amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Undo()
        {
            if (!succeeded) return;
            switch (action)
            {
                case Action.Deposit:
                    account.Withdraw(amount);
                    break;
                case Action.Withdraw:
                    account.Deposit(amount);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public class CommandDemo
    {
        public void Execute()
        {
            var ba = new BankAccount();
            var commands = new List<ICommand>
            {
                new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 100),
                new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 50)
            };

            Console.WriteLine(ba);

            foreach (var c in commands)
                c.Call();

            Console.WriteLine(ba);

            foreach (var c in Enumerable.Reverse(commands))
                c.Undo();

            Console.WriteLine(ba);
        }
    }


    public interface IControlCommand
    {
        void Execute();
    }
    public abstract class Control : IControlCommand
    {
        public Point Location { get; set; }
        public int Width { get; set; }
        public string Text { get; set; }

        public abstract void Execute();
    }
    public class ExitButton : Control
    {
        public override void Execute()
        {
            Console.WriteLine("Exit Environment...");
        }
    }
    public class MaxButton : Control
    {
        public override void Execute()
        {
            Console.WriteLine("Maximizing window...");
        }
    }

    public abstract class Control2 : IControlCommand
    {
        public abstract void Execute();
    }
    public class Timer :Control2
    {
        public override void Execute()
        {
            Console.WriteLine("Time ticking...");
        }
    }

    public class Window
    {
        public IControlCommand CloseButton;
        public IControlCommand MaximizeButton;
        public IControlCommand timer;

        public Window()
        {
            CloseButton = new ExitButton()
            {
                Text = "Çıkış",
                Location = new Point(200, 200),
                Width = 100
            };

            MaximizeButton = new MaxButton()
            {
                Text = "Max",
                Location = new Point(1200, 10),
                Width = 50
            };
            timer = new Timer();
        }
    }
    public class WindowRunner
    {
        public void Execute()
        {
            var window = new Window();
            window.MaximizeButton.Execute();
            window.CloseButton.Execute();
        }
    }
}