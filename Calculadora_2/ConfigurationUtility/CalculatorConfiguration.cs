using Calculadora_2.Interfaces;
using System.Configuration;

namespace Calculadora_2.ConfigurationUtility
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
