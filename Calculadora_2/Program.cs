using Calculadora.Interfaces;
using Calculadora.Managers;
using System;
using Calculadora.Dependency;
using Calculadora.Processors;
using Calculadora.ConfigurationUtility;



namespace Calculadora {

    internal class Program {

        private static void Main(string[] args) {
            
            Injection();

            ICalculatorManager manager = DependencyInjector.Get<ICalculatorManager>();
            manager.Execute();
            
            Console.WriteLine("Enter para sair");
            Console.ReadLine();
        }

        public static void Injection()
        {
            DependencyInjector.Register<ICalculatorManager, CalculatorManager>();
            DependencyInjector.Register<IRabbitProcessor, RabbitProcessor>();
            DependencyInjector.Register<ICalculatorProcessor, CalculatorProcessor>();
            DependencyInjector.Register<ICalculatorConfiguration, CalculatorConfiguration>();
                        
        }
    }
}