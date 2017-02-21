using CalculadoraAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora.Interfaces {
    public interface ICalculatorProcessor {
        Resultado ProcessCalc(string conta);

    }
}
