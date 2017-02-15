using Calculadora_2.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_2.Processors {
    class CalculatorProcessor : ICalculatorProcessor {

        /// <summary>
        /// Processar a string em uma conta matematica
        /// </summary>
        /// <param name="conta">A conta em formato string</param>
        /// <returns></returns>
        public string ProcessCalc(string conta) {
            string[] numeros = conta.Split(';');

            if (numeros[2] != "+" && numeros[2] != "-" && numeros[2] != "*" && numeros[2] != "/") {
                return "Operacao Invalida";
            } else {
                return Calculate(numeros[2], Convert.ToDouble(numeros[0]), Convert.ToDouble(numeros[1])).ToString();
            }
        }

        /// <summary>
        /// Realizar operacao matematica
        /// </summary>
        /// <param name="operacao">Qual operacao será feita + - / *</param>
        /// <param name="numero1">Primeiro Valor da operacao</param>
        /// <param name="numero2">Segundo Valor da operacao</param>
        /// <returns></returns>
        private static double Calculate(string operacao, double numero1, double numero2) {
            switch (operacao) {
                case "+": return numero1 + numero2;
                case "-": return numero1 - numero2;
                case "*": return numero1 * numero2;
                case "/": return numero1 / numero2;
                default: return 0;
            }
        }
    }
}
