
using CalculadoraAPI.Interfaces;
using CalculadoraAPI.Manager;
using CalculadoraAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CalculadoraAPI.Controllers
{
    public class CalculatorAPIController : ApiController
    {
        [HttpPost]        
        public Task<Resultado> Calculator(Conta conta)
        {
            ICalculatorAPIManager manager = Calculadora.Dependency.DependencyInjector.Get<ICalculatorAPIManager>();
            return manager.Execute(conta);
        }
    }
}
