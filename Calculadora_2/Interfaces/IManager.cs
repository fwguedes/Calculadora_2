using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora_2.Interfaces {
    interface IManager {

        void ConnectServer();
        void ConnectQueue();
        void DefineQoS(uint prefechSize, ushort prefechCount, bool global);
        void Start();
        void Execute();
    }
}
