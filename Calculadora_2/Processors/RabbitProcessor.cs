using Calculadora.Interfaces;
using CalculadoraAPI.Models;
using DNAuth.StackLibrary.UtilityStack.Library;
using DNAuth.StackLibrary.UtilityStack.Library.Enums;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Calculadora.Processors {

    internal class RabbitProcessor : IRabbitProcessor {
        private IConnectionFactory factory;
        private IConnection connection;
        private IModel channel;
        private EventingBasicConsumer consumer;
        private string queueName;
        private ICalculatorProcessor calculatorProcessor;
        private ICalculatorConfiguration calcConfig;
        private delegate string responseCallBack(string message);


        /// <summary>
        ///Conectar com o "servidor" do Rabbit
        ///Criar o canal para esta conexao
        /// </summary>
        /// <param name="hostname">Endereco do "servidor"</param>
        public void ConnectServer() {

            calcConfig = Dependency.DependencyInjector.Get<ICalculatorConfiguration>();

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

        /// <summary>
        /// Começa a ouvir a fila e a processar as mensagens recebidas
        /// </summary>
        public void Start() {
            if (connection == null) {
                throw new Exception("Não há servidor conectado");
            }

            consumer = new EventingBasicConsumer(channel);

            channel.BasicConsume(queue: queueName,
                                noAck: false,
                                consumer: consumer);

            Console.WriteLine("Ouvindo...");

            MessageReceived();
        }

        /// <summary>
        /// Evento de quando uma mensagem é recebida da fila
        /// </summary>
        private void MessageReceived() {
            calculatorProcessor = Dependency.DependencyInjector.Get<ICalculatorProcessor>();            
            consumer.Received += (model, ea) => {
                var message = Encoding.UTF8.GetString(ea.Body);
                Console.WriteLine("Recebido : {0}", message);
                Response(calculatorProcessor.ProcessCalc(message), ea);
                                
            };
        }

        /// <summary>
        /// Resposta ao "cliente" que enviou a mensagem
        /// </summary>
        /// <param name="message">Mensagem a ser enviada</param>
        /// <param name="ea">Argumentos do Evento</param>
        private void Response(Resultado message, BasicDeliverEventArgs ea) {
            var responseBytes = StringUtility.ObjectToJsonBuffed(message);
            var replyProps = channel.CreateBasicProperties();
            replyProps.CorrelationId = ea.BasicProperties.CorrelationId;
            
            channel.BasicPublish(exchange: "",
                                routingKey: ea.BasicProperties.ReplyTo,
                                basicProperties: replyProps,
                                body: responseBytes);

            channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);

            Console.WriteLine("Enviado : {0}", message);
        }
    }
}