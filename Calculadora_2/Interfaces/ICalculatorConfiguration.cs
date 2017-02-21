using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_2.Interfaces
{
    public interface ICalculatorConfiguration
    {
        string HostName { get; }
        string UserName { get; }
        string Password { get; }
        string VirtualHost { get; }        
        string QueueName { get; }
    }
}

