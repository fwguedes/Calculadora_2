using CalculadoraAPI.Interfaces;
using CalculadoraAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CalculadoraAPI.Manager
{
    public class CalculatorAPIManager : ICalculatorAPIManager
    {

        public Task<Resultado> Execute(Conta conta)
        {

            ICalculatorAPIProcessor processor = Calculadora.Dependency.DependencyInjector.Get<ICalculatorAPIProcessor>();
            return processor.Execute(conta);
        }
    }
}