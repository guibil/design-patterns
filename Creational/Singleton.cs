using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns.Creational
{
    public interface IExecution
    {
        void Execute();
    }

    // By using Dependency Injection Container...
    public class Singleton : IExecution
    {
        public void Execute()
        {
            Console.WriteLine("Executing...");
        }
    }

    // El classico
    public class VeryBusyDatabase
    {
        private static VeryBusyDatabase instance = new VeryBusyDatabase();

        public static VeryBusyDatabase GetInstance()
        {
            return instance;
        }

        private VeryBusyDatabase() { }
    }

    public class VeryBusyDatabaseT2
    {
        private static readonly object SyncLock = new object();
        private static VeryBusyDatabaseT2 instance = null;

        public static VeryBusyDatabaseT2 GetInstance()
        {
            lock (SyncLock)
            {
                if (instance == null)
                {
                    instance = new VeryBusyDatabaseT2();
                }
            }

            return instance;
        }

        private VeryBusyDatabaseT2() { }
    }

    public class VeryBusyDatabaseT3
    {
        private static Lazy<VeryBusyDatabaseT3> instance =
            new Lazy<VeryBusyDatabaseT3>(() => new VeryBusyDatabaseT3());

        public static VeryBusyDatabaseT3 GetInstance()
        {
            return instance.Value;
        }

        private VeryBusyDatabaseT3() { }
    }

    public class MyTask
    {
        private IExecution c;

        public MyTask(IExecution c)
        {
            this.c = c;
        }
    }
}
