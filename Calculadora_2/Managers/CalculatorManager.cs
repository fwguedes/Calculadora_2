using Calculadora.Interfaces;
using Calculadora.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora.Managers
{
    class CalculatorManager : ICalculatorManager
    {

        IRabbitProcessor rabbitProcessor;
        ICalculatorConfiguration calcConfig;

        /// <summary>
        /// Conecta com o "servidor"
        /// </summary>        
        /// <returns></returns>
        public void ConnectServer()
        {
            calcConfig = Dependency.DependencyInjector.Get<ICalculatorConfiguration>();
            rabbitProcessor = Dependency.DependencyInjector.Get<IRabbitProcessor>();
            rabbitProcessor.ConnectServer();
            
        }

        /// <summary>
        /// Conecta com a fila
        /// </summary>        
        /// <returns></returns>
        public void ConnectQueue()
        {
            rabbitProcessor.ConnectQueue();
            
        }

        /// <summary>
        /// Define a qualidade do servico'
        /// </summary>
        /// <param name="prefechSize">Tamanho maximo para envio antecipado</param>
        /// <param name="prefechCount">Quantidade maxima de taredas pendentes</param>
        /// <param name="global">Se a configuracao e para todo canal ou para cada 'consumer'</param>
        /// <returns></returns>
        public void DefineQoS(uint prefechSize, ushort prefechCount, bool global)
        {
            rabbitProcessor.DefineQoS(prefechSize, prefechCount, global);
            
        }

        /// <summary>
        /// Inicia o 'consumer'
        /// </summary>
        /// <returns></returns>
        public void Start()
        {
            rabbitProcessor.Start();
            
        }

        public void Execute()
        {
            ConnectServer();
            ConnectQueue();
            DefineQoS(0, 1, true);
            Start();            
        }
    }
}
