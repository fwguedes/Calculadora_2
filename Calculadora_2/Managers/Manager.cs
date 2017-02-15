using Calculadora_2.Interfaces;
using Calculadora_2.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_2.Managers {
    class Manager : IManager {

        IRabbitProcessor rabbitProcessor;
        ICalculatorProcessor calculatorProcessor;

        /// <summary>
        /// Constroi o Manager e seus processors
        /// </summary>
        public Manager() {
            rabbitProcessor = new RabbitProcessor();
            calculatorProcessor = new CalculatorProcessor();
        }
        
        /// <summary>
        /// Conecta com o "servidor"
        /// </summary>
        /// <param name="hostname">Endereco do Servidor</param>
        /// <returns></returns>
        public IManager ConnectServer(string hostname) {
            rabbitProcessor.ConnectServer(hostname);
            return this;
        }

        /// <summary>
        /// Conecta com a fila
        /// </summary>
        /// <param name="queueName">Nome da Fila</param>
        /// <returns></returns>
        public IManager ConnectQueue(string queueName) {
            rabbitProcessor.ConnectQueue(queueName);
            return this;
        }

        /// <summary>
        /// Define a qualidade do servico'
        /// </summary>
        /// <param name="prefechSize">Tamanho maximo para envio antecipado</param>
        /// <param name="prefechCount">Quantidade maxima de taredas pendentes</param>
        /// <param name="global">Se a configuracao e para todo canal ou para cada 'consumer'</param>
        /// <returns></returns>
        public IManager DefineQoS(uint prefechSize, ushort prefechCount, bool global) {
            rabbitProcessor.DefineQoS(prefechSize, prefechCount, global);
            return this;
        }

        /// <summary>
        /// Inicia o 'consumer'
        /// </summary>
        /// <returns></returns>
        public IManager Start() {
            rabbitProcessor.Start();
            return this;
        }
        
    }
}
