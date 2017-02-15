using Calculadora_2.Interfaces;
using Calculadora_2.Managers;
using System;

namespace Calculadora_2 {

    internal class Program {

        private static void Main(string[] args) {
            IManager manager = new Manager();

            manager.ConnectServer("localhost")
                .ConnectQueue("calcFila")
                .DefineQoS(0, 1, false)
                .Start();

            Console.WriteLine("Enter para sair");
            Console.ReadLine();
        }
    }
}