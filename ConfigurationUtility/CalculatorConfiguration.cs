using Calculadora.Interfaces;
using System.Configuration;

namespace Calculadora.ConfigurationUtility
{
    public class CalculatorConfiguration : ICalculatorConfiguration
    {
        public string HostName =>   ConfigurationManager.AppSettings["HostName"];
        
        public string Password => ConfigurationManager.AppSettings["Password"];
        
        public string UserName => ConfigurationManager.AppSettings["UserName"];
        
        public string VirtualHost => ConfigurationManager.AppSettings["VirtualHost"];

        public string QueueName => ConfigurationManager.AppSettings["QueueName"];

    }
}
