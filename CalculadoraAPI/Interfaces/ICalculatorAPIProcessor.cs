using CalculadoraAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculadoraAPI.Interfaces
{
    interface ICalculatorAPIProcessor
    {
        Task<Resultado> Execute(Conta conta);
    }
}
