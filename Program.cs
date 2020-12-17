using System;
using DesignPatterns.Behavioral;
using DesignPatterns.Creational;
using DesignPatterns.Structural;
using Microsoft.Extensions.DependencyInjection;

namespace DesignPatterns
{
    internal class Program
    {
        private static ServiceProvider serviceProvider;
        private static void RegisterTypes()
        {
            serviceProvider = new ServiceCollection()
                .AddSingleton<IExecution, Singleton>()
                //.AddTransient<MyTask>()
                .BuildServiceProvider();

        }

        static void Main(string[] args)
        {
            //RegisterTypes();

            //var t = serviceProvider.GetService<IExecution>();
            //t.Execute();
            //new PizzaBakerDemo().Run();

            new AdapterExecution().Execute();


            Console.WriteLine("Hello World!");
        }
    }
}
