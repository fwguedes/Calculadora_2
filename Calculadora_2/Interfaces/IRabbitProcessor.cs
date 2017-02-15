using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_2.Interfaces {
    interface IRabbitProcessor {
        void ConnectServer(string hostname);
        void ConnectQueue(string queueName);
        void DefineQoS(uint prefechSize, ushort prefechCount, bool global);
        void Start();


    }
}
