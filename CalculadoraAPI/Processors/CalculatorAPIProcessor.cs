using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Calculadora.Dependency;
using CalculadoraAPI.Models;
using CalculadoraAPI.Interfaces;
using Calculadora.Interfaces;
using System.Threading.Tasks;

namespace CalculadoraAPI.Processors
{
    public class CalculatorAPIProcessor : ICalculatorAPIProcessor
    {

        public Task<Resultado> Execute(Conta conta)
        {
            var responseTask = Task.Factory.StartNew(() =>
            {
                IRabbitAPIProcessor rabbitProcessor = Calculadora.Dependency.DependencyInjector.Get<IRabbitAPIProcessor>();

                rabbitProcessor.ConnectServer();
                rabbitProcessor.ConnectQueue();
                rabbitProcessor.DefineQoS(0, 1, true);

                return rabbitProcessor.SendRPC<Resultado>(conta);
            });

            return responseTask;

            
        }
           

    }
}