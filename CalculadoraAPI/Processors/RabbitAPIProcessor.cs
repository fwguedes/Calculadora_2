using CalculadoraAPI.Interfaces;
using CalculadoraAPI.Models;
using DNAuth.StackLibrary.UtilityStack.Library;
using DNAuth.StackLibrary.UtilityStack.Library.Enums;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Calculadora.ConfigurationUtility;
using Calculadora.Interfaces;
using System.Threading.Tasks;
using System.Threading;

namespace CalculadoraAPI.Processors {

    internal class RabbitAPIProcessor : IRabbitAPIProcessor {
        private IConnectionFactory factory;
        private IConnection connection;
        private IModel channel;
        private EventingBasicConsumer consumer;
        private string queueName;
        private ICalculatorConfiguration calcConfig;
        private delegate string responseCallBack(string message);


        /// <summary>
        ///Conectar com o "servidor" do Rabbit
        ///Criar o canal para esta conexao
        /// </summary>
        /// <param name="hostname">Endereco do "servidor"</param>
        public void ConnectServer() {

            calcConfig = Calculadora.Dependency.DependencyInjector.Get<ICalculatorConfiguration>();

            factory = new ConnectionFactory() { HostName = calcConfig.HostName, VirtualHost = calcConfig.VirtualHost };

            factory.UserName = CredeltialsRegistryUtility.GetValue(ServiceName.RabbitMQ, calcConfig.UserName );
            factory.Password = CredeltialsRegistryUtility.GetValue(ServiceName.RabbitMQ, calcConfig.Password );

            connection = factory.CreateConnection();
            channel = connection.CreateModel();
        }

        /// <summary>
        /// Criacao da fila
        /// </summary>
        /// <param name="queueName">Nome da Fila</param>
        public void ConnectQueue() {
            if (connection == null) {
                throw new Exception("Não há servidor conectado");
            }

            this.queueName = calcConfig.QueueName;

            channel.QueueDeclare(queue: this.queueName,
                                durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        /// <summary>
        /// Definindo a "Qualidade de Serviço" do canal
        /// </summary>
        /// <param name="prefechSize">Tamanho maximo para uma mensagem ser anticepada para o envio.0 nenhum mensagem é antecipada</param>
        /// <param name="prefechCount">Quantas tarefas cada "Consumer" pode ter pendentes</param>
        /// <param name="global">Se as configuracoes sao aplicadas para todo canal(true) ou para cada "Consumer"(false)</param>
        public void DefineQoS(uint prefechSize, ushort prefechCount, bool global) {
            if (connection == null) {
                throw new Exception("Não há servidor conectado");
            }

            channel.BasicQos(prefechSize, prefechCount, global);
        }
        
        public TResponse SendRPC<TResponse>(Conta conta) where TResponse : class
        {
            consumer = new EventingBasicConsumer(channel);
            var resetEvent = new ManualResetEvent(false);
            var answer = default(TResponse);

            var replyQueueName = channel.QueueDeclare().QueueName;

            

            var corrID = Guid.NewGuid().ToString();
            var props = channel.CreateBasicProperties();
            props.ReplyTo = replyQueueName;
            props.CorrelationId = corrID;
            

            //Criando a mensagem
            var contaBytes = Encoding.UTF8.GetBytes(conta.Valor1.ToString() + ";" + conta.Valor2.ToString() + ";" + conta.Operacao.ToString());
            //Publicando na fila
            channel.BasicPublish(
                exchange: "",
                routingKey: "calcFila",
                basicProperties: props,
                body: contaBytes);

            consumer.Received += (model, ea) =>
            {
                if (ea.BasicProperties.CorrelationId != corrID) return;

                var message = Encoding.UTF8.GetString(ea.Body);

                answer = Newtonsoft.Json.JsonConvert.DeserializeObject<TResponse>(message);

                resetEvent.Set();
            };

            channel.BasicConsume(queue: replyQueueName,
                                noAck: true,
                                consumer: consumer);

            if (resetEvent.WaitOne(900000) == false)
            {
                throw new Exception ($"Queue RPC Flow timemout. Queue {queueName} ");
            }

            return answer;
        }
        
        private string ReceiveMessage(object model,BasicDeliverEventArgs  ea, string corrID)
        {
            if (ea.BasicProperties.CorrelationId == corrID)
            {
                return  Encoding.UTF8.GetString(ea.Body);
            }

            return null;
        }      
    }
}