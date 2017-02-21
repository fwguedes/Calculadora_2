using Calculadora_2.Interfaces;
using Calculadora_2.Managers;
using System;
using Calculadora_2.Dependency;
using Calculadora_2.Processors;
using Calculadora_2.ConfigurationUtility;

namespace Calculadora_2 {

    internal class Program {

        private static void Main(string[] args) {
            
            Injection();

            IManager manager = DependencyInjector.Get<IManager>();
            manager.Execute();
            
            Console.WriteLine("Enter para sair");
            Console.ReadLine();
        }

        public static void Injection()
        {
            DependencyInjector.Register<IManager, Manager>();
            DependencyInjector.Register<IRabbitProcessor, RabbitProcessor>();
            DependencyInjector.Register<ICalculatorProcessor, CalculatorProcessor>();
            DependencyInjector.Register<ICalculatorConfiguration, CalculatorConfiguration>();
                        
        }
    }
}