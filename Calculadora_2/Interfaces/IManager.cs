using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_2.Interfaces {
    interface IManager {

        IManager ConnectServer(string hostname);
        IManager ConnectQueue(string queueName);
        IManager DefineQoS(uint prefechSize, ushort prefechCount, bool global);
        IManager Start();

    }
}
